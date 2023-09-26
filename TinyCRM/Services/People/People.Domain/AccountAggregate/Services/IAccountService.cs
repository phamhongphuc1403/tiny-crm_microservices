using People.Domain.AccountAggregate.Entities;

namespace People.Domain.AccountAggregate.Services;

public interface IAccountService
{
    Task<Account> CreateAsync(string name, string? email, string? phone, string? address);
    Task<Account> EditAsync(Guid id, string name, string? email, string? phone, string? address);
    Task<IList<Account>> DeleteManyAsync(IEnumerable<Guid> ids);
}