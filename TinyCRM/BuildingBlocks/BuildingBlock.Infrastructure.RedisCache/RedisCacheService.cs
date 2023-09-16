using BuildingBlock.Infrastructure.RedisCache.Cache.Interface;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BuildingBlock.Infrastructure.RedisCache;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task<T?> GetRecordAsync<T>(string key)
    {
        var stringData = await _db.StringGetAsync(key);
        if (string.IsNullOrWhiteSpace(stringData)) return default;

        return JsonConvert.DeserializeObject<T>(stringData.ToString());
    }
}