namespace BuildingBlock.Infrastructure.RedisCache.Cache.Interface;

public interface IPermissionCacheManager
{
    Task<IEnumerable<string>?> GetPermissionsRoleAsync(string role);
    Task<IEnumerable<string>?> GetRolesUserAsync(string userId);
}