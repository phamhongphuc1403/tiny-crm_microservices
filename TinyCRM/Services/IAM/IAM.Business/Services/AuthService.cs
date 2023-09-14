using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuildingBlock.Domain.Exceptions;
using IAM.Business.Models;
using IAM.Business.Models.Dto.Users;
using IAM.Business.Services.IServices;
using IAM.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IAM.Business.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
        JwtSettings jwtSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings;
    }

    public async Task<string> SignInAsync(SignInDto signInDto)
    {
        var user = await _userManager.FindByEmailAsync(signInDto.Email)
                   ?? throw new EntityNotFoundException($"User with Email{signInDto.Email}] not found");

        var result = await _signInManager.PasswordSignInAsync(user, signInDto.Password, false, true);
        if (!result.Succeeded) throw new InvalidPasswordException("Invalid password");

        var roles = await _userManager.GetRolesAsync(user);

        var token = GenerateToken(user.Id, user.Email!, roles);
        return token;
    }

    private static IEnumerable<Claim> GenerateAuthClaims(string id, string email, IEnumerable<string> roles)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, id),
            new(ClaimTypes.Email, email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return authClaims;
    }

    private string GenerateToken(string id, string email, IEnumerable<string> roles)
    {
        var authClaims = GenerateAuthClaims(id, email, roles);
        var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var token = GenerateJwtToken(authClaims, authKey);
        return WriteJwtToken(token);
    }

    private JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> authClaims, SecurityKey authKey)
    {
        return new JwtSecurityToken(
            _jwtSettings.ValidIssuer,
            _jwtSettings.ValidAudience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
        );
    }

    private static string WriteJwtToken(SecurityToken token)
    {
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}