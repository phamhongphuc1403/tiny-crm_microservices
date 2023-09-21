using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BuildingBlock.Presentation.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly AuthGrpcService.AuthGrpcServiceClient _authGrpcServiceClient;

    public PermissionAuthorizationHandler(AuthGrpcService.AuthGrpcServiceClient authGrpcServiceClient,
        IHttpContextAccessor httpContextAccessor)
    {
        _authGrpcServiceClient = authGrpcServiceClient;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        // Get User Id in header
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine($"User Id: {userId}");
        if (userId != null)
        {
            var permissions = await _authGrpcServiceClient.GetPermissionsAsync(new PermissionRequest
            {
                UserId = userId
            });
            var isAuthorized = permissions.Permissions.ToList().Contains(requirement.Permission);
            if (isAuthorized) context.Succeed(requirement);
        }
    }
}