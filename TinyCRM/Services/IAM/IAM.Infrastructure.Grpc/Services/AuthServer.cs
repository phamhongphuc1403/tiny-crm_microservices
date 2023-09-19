using System.Security.Claims;
using BuildingBlock.Application.Identity;
using Grpc.Core;
using IAM.Business.Services.IServices;
using IAM.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IAM.Infrastructure.Grpc.Services;

public class AuthServer : AuthGrpcService.AuthGrpcServiceBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<AuthServer> _logger;
    private readonly IAuthService _authService;
    public AuthServer(ILogger<AuthServer> logger, IAuthService authService, UserManager<ApplicationUser> userManager, ICurrentUser currentUser)
    {
        _logger = logger;
        _authService = authService;
        _userManager = userManager;
        _currentUser = currentUser;
    }

    public override async Task GetClaims(Empty request, IServerStreamWriter<ClaimResponse> responseStream, ServerCallContext context)
    {
        if (!_currentUser.IsAuthenticated)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User is not authenticated"));    
        }
        
        var userId = _currentUser.Id;

        var user = await _userManager.FindByIdAsync(userId!);
        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found!"));
        }

        var claims = await GetUserClaims(user);

        foreach (var claim in claims)
        {
            await responseStream.WriteAsync(new ClaimResponse()
            {
                Type = claim.Type,
                Value = claim.Value
            });
        }
    }

    
    private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
        };

        // Todo: caching
        foreach (var role in await _userManager.GetRolesAsync(user))
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

        return claims;
    }
    
    public override async Task<PermissionResponse> GetPermissions(PermissionRequest request, ServerCallContext context)
    {
        var permissions = await _authService.GetPermissionsAsync(request.UserId);

        return await Task.FromResult(new PermissionResponse
        {
            Permissions = { permissions }
        });
    }
}