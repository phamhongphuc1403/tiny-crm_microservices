using BuildingBlock.Infrastructure.RedisCache;
using BuildingBlock.Infrastructure.RedisCache.Cache;
using BuildingBlock.Infrastructure.RedisCache.Cache.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BuildingBlock.Presentation.Extensions;

public static class RedisCacheExtension
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis") ?? string.Empty);

        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        services.AddTransient<ICacheService, RedisCacheService>();
        services.AddTransient<IPermissionCacheManager, PermissionCacheManager>();
        services.AddTransient<IMailSenderCacheManager,MailSenderCacheManager>();
        return services;
    }
}