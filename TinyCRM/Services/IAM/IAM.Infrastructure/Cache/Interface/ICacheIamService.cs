using BuildingBlock.Infrastructure.RedisCache.Cache.Interface;

namespace IAM.Infrastructure.Cache.Interface;

public interface ICacheIamService : ICacheService
{
    Task<bool> SetRecordAsync<T>(string key, T data, TimeSpan expireTime);
    Task<bool> RemoveRecordAsync(string key);

    Task ClearAsync();
}