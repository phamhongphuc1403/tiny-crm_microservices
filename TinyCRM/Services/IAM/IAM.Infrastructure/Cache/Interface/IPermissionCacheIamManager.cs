using BuildingBlock.Infrastructure.RedisCache.Cache.Interface;

namespace IAM.Infrastructure.Cache.Interface;

public interface IPermissionCacheIamManager : IPermissionCacheManager
{
    Task SetPermissionsRoleAsync(string role, IEnumerable<string> permissions);

    Task ClearPermissionsRoleAsync(string role);
    Task SetRolesUserAsync(string userId, IEnumerable<string> roles, TimeSpan expireTime);
    Task ClearRolesUserAsync(string userId);
}