using System.Reflection;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Infrastructure.EFCore;
using BuildingBlock.Infrastructure.RedisCache;
using BuildingBlock.Infrastructure.RedisCache.Cache.Interface;
using IAM.Business;
using IAM.Business.Services;
using IAM.Business.Services.IServices;
using IAM.Infrastructure.Cache;
using IAM.Infrastructure.Cache.Interface;
using IAM.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

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

    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis") ?? string.Empty);

        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        services.AddTransient<ICacheService, RedisCacheService>();
        services.AddTransient<ICacheIamService,RedisCacheIamService>();
        services.AddTransient<IPermissionCacheManager, PermissionCacheManager>();
        services.AddTransient<IPermissionCacheIamManager, PermissionCacheIamManager>();
        return services;
    }
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(IdentityBusinessAssemblyReference)));

        return services;
    }
}