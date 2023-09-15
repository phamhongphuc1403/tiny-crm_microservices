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

    public Task<bool> SetRecordAsync<T>(string key, T data, TimeSpan expireTime)
    {
        var serializedData = JsonConvert.SerializeObject(data);

        return _db.StringSetAsync(key, serializedData, expireTime, When.Always);
    }

    public Task<bool> RemoveRecordAsync(string key)
    {
        return _db.KeyDeleteAsync(key);
    }

    public async Task ClearAsync()
    {
        var endpoints = _redis.GetEndPoints(true);
        foreach (var endpoint in endpoints)
        {
            var server = _redis.GetServer(endpoint);
            await server.FlushAllDatabasesAsync();
        }
    }
}