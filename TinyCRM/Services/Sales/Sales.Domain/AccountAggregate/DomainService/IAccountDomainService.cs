namespace Sales.Domain.AccountAggregate.DomainService;

public interface IAccountDomainService
{
    Task<Account> CreateAsync(string name, string? email = null);
    Task<Account> CreateAsync(Guid id, string name, string? email);
    Task<Account> UpdateAsync(Account account, string name, string? email = null);
    Task<IList<Account>> DeleteManyAsync(IEnumerable<Guid> ids);
}