using BuildingBlock.Domain.Model;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using People.Domain.AccountAggregate.Exceptions;
using People.Domain.AccountAggregate.Specifications;
using People.Domain.ContactAggregate.Entities;

namespace People.Domain.AccountAggregate.Entities;

public class Account : GuidEntity
{
    private Account(string name, string? email, string? phone, string? address)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public Account()
    {
    }

    public string Name { get; private set; } = null!;
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }
    public double TotalSales { get; }

    public ICollection<Contact> Contacts { get; private set; } = null!;

    public static async Task<Account> CreateAsync(string name, string? email, string? phone, string? address,
        IReadOnlyRepository<Account> repository)
    {
        if (email != null) await CheckValidOnCreateExistAsync(email, repository);

        return new Account(name, email, phone, address);
    }

    public static async Task<Account> EditAsync(Guid id, string name, string? email, string? phone, string? address,
        IReadOnlyRepository<Account> repository)
    {
        var account = await CheckValidOnEditAsync(id, email, repository);

        account.Name = name;
        account.Email = email;
        account.Phone = phone;
        account.Address = address;

        return account;
    }

    private static async Task<Account> CheckValidOnEditAsync(Guid id, string? email,
        IReadOnlyRepository<Account> repository)
    {
        var accountIdSpecification = new AccountIdSpecification(id);

        var account = Optional<Account>.Of(await repository.GetAnyAsync(accountIdSpecification))
            .ThrowIfNotPresent(new AccountNotFoundException(id)).Get();

        if (email != null) await CheckEmailValidOnEditAsync(id, email, repository);

        return account;
    }

    private static async Task CheckValidOnCreateExistAsync(string email, IReadOnlyRepository<Account> repository)
    {
        var accountEmailSpecification = new AccountEmailExactMatchSpecification(email);

        Optional<bool>.Of(await repository.CheckIfExistAsync(accountEmailSpecification))
            .ThrowIfPresent(new AccountDuplicatedException(nameof(email), email));
    }

    private static async Task CheckEmailValidOnEditAsync(Guid id, string email,
        IReadOnlyRepository<Account> repository)
    {
        var accountEmailSpecification = new AccountEmailExactMatchSpecification(email);

        var accountIdNotEqualSpecification = new AccountIdNotEqualSpecification(id);

        var specification = accountEmailSpecification.And(accountIdNotEqualSpecification);

        Optional<bool>.Of(await repository.CheckIfExistAsync(specification))
            .ThrowIfPresent(new AccountDuplicatedException(nameof(email), email));
    }

    public static async Task<IList<Account>> DeleteManyAsync(IEnumerable<Guid> ids,
        IReadOnlyRepository<Account> readonlyRepository)
    {
        List<Account> accounts = new();

        foreach (var id in ids)
        {
            var accountIdSpecification = new AccountIdSpecification(id);

            var account = Optional<Account>.Of(await readonlyRepository.GetAnyAsync(accountIdSpecification))
                .ThrowIfNotPresent(new AccountNotFoundException(id)).Get();

            accounts.Add(account);
        }

        return accounts;
    }
}