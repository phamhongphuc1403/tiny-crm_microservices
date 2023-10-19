using BuildingBlock.Infrastructure.RedisCache;
using IAM.Infrastructure.Cache.Interface;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace IAM.Infrastructure.Cache;

public class RedisCacheIamService : RedisCacheService, ICacheIamService
{
    private readonly IDatabase _db;
    private readonly IConnectionMultiplexer _redis;

    public RedisCacheIamService(IConnectionMultiplexer redis) : base(redis)
    {
        _redis = redis;
        _db = redis.GetDatabase();
    }
}