using System.Security.Claims;
using BuildingBlock.Presentation.Authorization;
using IAM.Business.Services.IServices;
using Microsoft.AspNetCore.Authorization;

namespace IAM.API.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IAuthService _authService;

    public PermissionAuthorizationHandler(IAuthService authService)
    {
        _authService = authService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != null)
        {
            var permissions = await _authService.GetPermissionsAsync(userId);

            var isAuthorized = permissions.Contains(requirement.Permission);
            if (isAuthorized) context.Succeed(requirement);
        }
    }
}