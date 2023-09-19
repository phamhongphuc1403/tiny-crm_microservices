using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.Authentication;

public static class AuthenticationExceptions
{
    public static IServiceCollection AddTinyCRMAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<AuthGrpcService.AuthGrpcServiceClient>((provider, options) =>
        {
            var identityUrl = configuration["GrpcUrls:Identity"];
            options.Address = new Uri(identityUrl!);
        });
            
        services.AddAuthentication(AuthenticationDefaults.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>(AuthenticationDefaults.AuthenticationScheme, null);

        return services;
    }
}