using System.Reflection;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Infrastructure.EFCore;
using IAM.Business;
using IAM.Business.Services;
using IAM.Business.Services.IServices;
using IAM.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace IAM.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            }
        );
        services.AddScoped<DataContributor>();
        // services.AddScoped<PermissionContributor>();

        services.AddScoped<Func<IdentityDataContext>>(provider => () => provider.GetService<IdentityDataContext>()
                                                                        ?? throw new InvalidOperationException());
        services.AddScoped<IUnitOfWork, UnitOfWork<IdentityDataContext>>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IAuthService, AuthService>();
    }

    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(IdentityBusinessAssemblyReference)));

        return services;
    }
}