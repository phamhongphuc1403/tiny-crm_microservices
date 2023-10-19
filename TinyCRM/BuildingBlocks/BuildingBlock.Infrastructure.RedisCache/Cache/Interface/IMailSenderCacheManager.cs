namespace BuildingBlock.Infrastructure.RedisCache.Cache.Interface;

public interface IMailSenderCacheManager
{
    Task SetStatusMailSenderAsync(string mailSenderRabbitMq);
    Task<bool?> CheckStatusMailSenderAsync(string mailSenderRabbitMq);
    Task<bool> RemoveMailSenderAsync(string mailSenderRabbitMq);
}