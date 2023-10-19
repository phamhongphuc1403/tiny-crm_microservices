using BuildingBlock.Infrastructure.RedisCache.Cache.Interface;

namespace BuildingBlock.Infrastructure.RedisCache.Cache;

public class MailSenderCacheManager : IMailSenderCacheManager
{
    private readonly ICacheService _cacheService;

    public MailSenderCacheManager(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task SetStatusMailSenderAsync(string mailSenderRabbitMq)
    {
        await _cacheService.SetRecordAsync(KeyGenerator.Generate(CacheTarget.MailSenderRabbitMq, mailSenderRabbitMq),
            true, TimeSpan.FromMinutes(30));
    }

    public async Task<bool?> CheckStatusMailSenderAsync(string mailSenderRabbitMq)
    {
        return await _cacheService.GetRecordAsync<bool?>(KeyGenerator.Generate(CacheTarget.MailSenderRabbitMq, mailSenderRabbitMq));
    }

    public async Task<bool> RemoveMailSenderAsync(string mailSenderRabbitMq)
    {
        return await _cacheService.RemoveRecordAsync(KeyGenerator.Generate(CacheTarget.MailSenderRabbitMq, mailSenderRabbitMq));
    }
}