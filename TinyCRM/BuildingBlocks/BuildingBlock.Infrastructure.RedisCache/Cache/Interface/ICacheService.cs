namespace BuildingBlock.Infrastructure.RedisCache.Cache.Interface;

public interface ICacheService
{
    Task<T?> GetRecordAsync<T>(string key);
    Task<bool> SetRecordAsync<T>(string key, T data, TimeSpan expireTime);
    Task<bool> RemoveRecordAsync(string key);
    Task ClearAsync();
}