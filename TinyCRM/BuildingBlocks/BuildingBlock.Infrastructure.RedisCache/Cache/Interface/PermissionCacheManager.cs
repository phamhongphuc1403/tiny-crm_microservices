namespace BuildingBlock.Infrastructure.RedisCache.Cache.Interface;

public class PermissionCacheManager : IPermissionCacheManager
{
    private readonly ICacheService _cacheService;

    public PermissionCacheManager(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public Task<IEnumerable<string>?> GetPermissionsRoleAsync(string role)
    {
        return _cacheService.GetRecordAsync<IEnumerable<string>?>
            (KeyGenerator.Generate(CacheTarget.PermissionRole, role));
    }

    public Task<IEnumerable<string>?> GetRolesUserAsync(string userId)
    {
        return _cacheService.GetRecordAsync<IEnumerable<string>?>
            (KeyGenerator.Generate(CacheTarget.RoleUser, userId));
    }
}