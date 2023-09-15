namespace BuildingBlock.Infrastructure.RedisCache.Cache.Interface;

public interface ICacheService
{
    Task<T?> GetRecordAsync<T>(string key);
}