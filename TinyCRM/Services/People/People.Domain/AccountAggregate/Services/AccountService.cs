using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Exceptions;
using People.Domain.AccountAggregate.Specifications;

namespace People.Domain.AccountAggregate.Services;

public class AccountService : IAccountService
{
    private readonly IReadOnlyRepository<Account> _readOnlyRepository;

    public AccountService(IReadOnlyRepository<Account> readOnlyRepository)
    {
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<Account> CreateAsync(string name, string? email, string? phone, string? address)
    {
        if (email != null) await CheckValidOnCreateAsync(email);

        return new Account(name, email, phone, address);
    }

    public async Task<Account> EditAsync(Guid id, string name, string? email, string? phone, string? address)
    {
        var account = await CheckValidOnEditAsync(id, email);

        account.Name = name;
        account.Email = email;
        account.Phone = phone;
        account.Address = address;

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

    private async Task CheckValidOnCreateAsync(string email)
    {
        var accountEmailSpecification = new AccountEmailExactMatchSpecification(email);

        Optional<bool>.Of(await _readOnlyRepository.CheckIfExistAsync(accountEmailSpecification))
            .ThrowIfPresent(new AccountDuplicatedException(nameof(email), email));
    }

    private async Task<Account> CheckValidOnEditAsync(Guid id, string? email)
    {
        var accountIdSpecification = new AccountIdSpecification(id);

        var account = Optional<Account>.Of(await _readOnlyRepository.GetAnyAsync(accountIdSpecification))
            .ThrowIfNotPresent(new AccountNotFoundException(id)).Get();

        if (email != null) await CheckEmailValidOnEditAsync(id, email);

        return account;
    }

    private async Task CheckEmailValidOnEditAsync(Guid id, string email)
    {
        var accountEmailSpecification = new AccountEmailExactMatchSpecification(email);

        var accountIdNotEqualSpecification = new AccountIdNotEqualSpecification(id);

        var specification = accountEmailSpecification.And(accountIdNotEqualSpecification);

        Optional<bool>.Of(await _readOnlyRepository.CheckIfExistAsync(specification))
            .ThrowIfPresent(new AccountDuplicatedException(nameof(email), email));
    }
}