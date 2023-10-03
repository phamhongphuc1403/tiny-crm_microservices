using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuildingBlock.Domain.Exceptions;
using IAM.Business.Models;
using IAM.Business.Models.Users.Dto;
using IAM.Business.Services.IServices;
using IAM.Domain.Entities.Roles;
using IAM.Domain.Entities.Users;
using IAM.Infrastructure.Cache.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IAM.Business.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IPermissionCacheIamManager _permissionCacheManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;


    public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
        JwtSettings jwtSettings,
        RoleManager<ApplicationRole> roleManager, IPermissionCacheIamManager permissionCacheManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings;
        _roleManager = roleManager;
        _permissionCacheManager = permissionCacheManager;
    }

    public async Task<string> SignInAsync(SignInDto signInDto)
    {
        var user = await _userManager.FindByEmailAsync(signInDto.Email)
                   ?? throw new EntityNotFoundException($"User with Email[{signInDto.Email}] not found");

        var result = await _signInManager.PasswordSignInAsync(user, signInDto.Password, false, true);
        if (!result.Succeeded) throw new InvalidPasswordException("Invalid password");

        var token = GenerateToken(user.Id, user.Email!, signInDto.RememberMe);
        return token;
    }


    public async Task<IEnumerable<string>> GetPermissionsAsync(string userId)
    {
        var permissions = new List<string>();
        var roles = await GetRolesAsync(userId);
        foreach (var role in roles)
        {
            var rolePermissions = await _permissionCacheManager.GetPermissionsRoleAsync(role);
            if (rolePermissions == null)
            {
                rolePermissions = await GetPermissionsRoleAsync(role);
                await _permissionCacheManager.SetPermissionsRoleAsync(role, rolePermissions);
            }

            permissions.AddRange(rolePermissions);
        }

        return permissions;
    }

    private async Task<List<string>> GetPermissionsRoleAsync(string role)
    {
        var roleEntity = await _roleManager.FindByNameAsync(role)
                         ?? throw new EntityNotFoundException($"Role with name {role} not found");

        var claims = await _roleManager.GetClaimsAsync(roleEntity);
        return (from claim in claims where claim.Type == "Permission" select claim.Value).ToList();
    }

    private async Task<IList<string>> GetRolesAsync(string userId)
    {
        var roles = await _permissionCacheManager.GetRolesUserAsync(userId);
        if (roles != null) return (IList<string>)roles;

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new EntityNotFoundException($"User with id {userId} not found");

        roles = await _userManager.GetRolesAsync(user);
        await _permissionCacheManager.SetRolesUserAsync(userId, roles,
            TimeSpan.FromMinutes(_jwtSettings.ExpiryInMinutes));
        return (IList<string>)roles;
    }

    private static IEnumerable<Claim> GenerateAuthClaims(string id, string email)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, id),
            new(ClaimTypes.Email, email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        return authClaims;
    }

    private string GenerateToken(string id, string email, bool rememberMe)
    {
        var authClaims = GenerateAuthClaims(id, email);
        var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var token = GenerateJwtToken(authClaims, authKey, rememberMe);
        return WriteJwtToken(token);
    }

    private JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> authClaims, SecurityKey authKey, bool rememberMe)
    {
        var expiryInMinutes = rememberMe ? _jwtSettings.RememberExpiryInMinutes : _jwtSettings.ExpiryInMinutes;
        return new JwtSecurityToken(
            _jwtSettings.ValidIssuer,
            _jwtSettings.ValidAudience,
            expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
        );
    }

    private static string WriteJwtToken(SecurityToken token)
    {
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}