using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Domain.AccountAggregate.Exceptions;
using Sales.Domain.AccountAggregate.Specifications;

namespace Sales.Domain.AccountAggregate.DomainService;

public class AccountDomainService : IAccountDomainService
{
    private readonly IReadOnlyRepository<Account> _readOnlyRepository;

    public AccountDomainService(IReadOnlyRepository<Account> readOnlyRepository)
    {
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<Account> CreateAsync(string name, string? email = null)
    {
        await CheckValidOnCreateExistAsync(email);
        return new Account(name, email);
    }

    public async Task<Account> CreateAsync(Guid id, string name, string? email)
    {
        await CheckValidOnCreateExistAsync(email);
        return new Account(id, name, email);
    }

    public async Task<Account> UpdateAsync(Account account, string name, string? email = null)
    {
        await CheckValidOnEditExistAsync(account.Id, email);
        account.Update(name, email);
        return account;
    }

    public async Task<IList<Account>> DeleteManyAsync(IEnumerable<Guid> ids)
    {
        List<Account> accounts = new();

        foreach (var id in ids)
        {
            var accountIdSpecification = new AccountIdSpecification(id);

            var account = Optional<Account>.Of(await _readOnlyRepository.GetAnyAsync(accountIdSpecification))
                .ThrowIfNotPresent(new AccountNotFoundException(id)).Get();

            accounts.Add(account);
        }

        return accounts;
    }

    private async Task CheckValidOnCreateExistAsync(string? email)
    {
        var accountEmailSpecification = new AccountEmailExactMatchSpecification(email);

        Optional<bool>.Of(await _readOnlyRepository.CheckIfExistAsync(accountEmailSpecification))
            .ThrowIfPresent(new AccountDuplicatedException(nameof(email), email ?? string.Empty));
    }

    private async Task CheckValidOnEditExistAsync(Guid id, string? email)
    {
        var accountEmailSpecification = new AccountEmailExactMatchSpecification(email);

        var accountIdNotEqualSpecification = new AccountIdNotEqualSpecification(id);

        var specification = accountEmailSpecification.And(accountIdNotEqualSpecification);

        Optional<bool>.Of(await _readOnlyRepository.CheckIfExistAsync(specification))
            .ThrowIfPresent(new AccountDuplicatedException(nameof(email), email ?? string.Empty));
    }
}