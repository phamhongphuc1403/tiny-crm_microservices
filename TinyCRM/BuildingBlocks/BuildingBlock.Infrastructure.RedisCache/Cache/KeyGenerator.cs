namespace BuildingBlock.Infrastructure.RedisCache.Cache;

public static class KeyGenerator
{
    public static string Generate(CacheTarget cacheTarget, string id)
    {
        return $"{cacheTarget}:{id}";
    }
}