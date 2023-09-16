using Grpc.Core;
using IAM.Business.Services.IServices;
using Microsoft.Extensions.Logging;

namespace IAM.Infrastructure.Grpc.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Saying hello to {Name}", request.Name);
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
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