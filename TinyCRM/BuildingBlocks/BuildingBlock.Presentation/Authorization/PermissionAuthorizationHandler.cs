using System.Security.Claims;
using BuildingBlock.Infrastructure.RedisCache;
using BuildingBlock.Infrastructure.RedisCache.Cache.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BuildingBlock.Presentation.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly AuthGrpcService.AuthGrpcServiceClient _authGrpcServiceClient;
    private readonly IPermissionCacheManager _permissionCacheManager;

    public PermissionAuthorizationHandler(AuthGrpcService.AuthGrpcServiceClient authGrpcServiceClient,
        IPermissionCacheManager permissionCacheManager)
    {
        _authGrpcServiceClient = authGrpcServiceClient;
        _permissionCacheManager = permissionCacheManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            bool isAuthorized;
            
            var permissionsCache = await GetPermissionsAsync(userId);
            if (permissionsCache != null)
            {
                isAuthorized = permissionsCache.Contains(requirement.Permission);
                if (isAuthorized) context.Succeed(requirement);
                return;
            }
            
            var permissions = await _authGrpcServiceClient.GetPermissionsAsync(new PermissionRequest
            {
                UserId = userId
            });
            isAuthorized = permissions.Permissions.ToList().Contains(requirement.Permission);
            if (isAuthorized) context.Succeed(requirement);
        }
    }

    private async Task<List<string>?> GetPermissionsAsync(string userId)
    {
        var roles = await _permissionCacheManager.GetRolesUserAsync(userId);
        if (roles == null)
        {
            return null;
        }

        var permissions = new List<string>();
        foreach (var role in roles)
        {
            var rolePermissions = await _permissionCacheManager.GetPermissionsRoleAsync(role);
            if (rolePermissions == null) return null;
            permissions.AddRange(rolePermissions);
        }

        return permissions;
    }
}