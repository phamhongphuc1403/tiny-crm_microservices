namespace Sales.Domain.AccountAggregate.DomainService;

public interface IAccountDomainService
{
    Task<Account> CreateAsync(string name, string? email = null);
    
}