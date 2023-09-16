using BuildingBlock.Infrastructure.RedisCache.Cache;
using BuildingBlock.Infrastructure.RedisCache.Cache.Interface;

namespace IAM.Infrastructure.Cache.Interface;

public class PermissionCacheIamManager : PermissionCacheManager, IPermissionCacheIamManager
{
    private readonly ICacheIamService _cacheIamService;

    public PermissionCacheIamManager(ICacheIamService cacheIamService) : base(cacheIamService)
    {
        _cacheIamService = cacheIamService;
    }

    public Task SetPermissionsRoleAsync(string role, IEnumerable<string> permissions)
    {
        return _cacheIamService.SetRecordAsync(KeyGenerator.Generate(CacheTarget.PermissionRole, role),
            permissions, TimeSpan.FromMinutes(21600));
    }

    public Task ClearPermissionsRoleAsync(string role)
    {
        return _cacheIamService.RemoveRecordAsync(KeyGenerator.Generate(CacheTarget.PermissionRole, role));
    }

    public Task SetRolesUserAsync(string userId, IEnumerable<string> roles, TimeSpan expireTime)
    {
        return _cacheIamService.SetRecordAsync(KeyGenerator.Generate(CacheTarget.RoleUser, userId),
            roles, expireTime);
    }

    public Task ClearRolesUserAsync(string userId)
    {
        return _cacheIamService.RemoveRecordAsync(KeyGenerator.Generate(CacheTarget.RoleUser, userId));
    }
}