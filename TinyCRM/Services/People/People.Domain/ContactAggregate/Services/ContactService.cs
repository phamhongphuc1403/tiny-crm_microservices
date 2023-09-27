using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Exceptions;
using People.Domain.AccountAggregate.Specifications;
using People.Domain.ContactAggregate.Entities;
using People.Domain.ContactAggregate.Exceptions;
using People.Domain.ContactAggregate.Specifications;

namespace People.Domain.ContactAggregate.Services;

public class ContactService : IContactService
{
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;
    private readonly IReadOnlyRepository<Contact> _contactReadOnlyRepository;

    public ContactService(IReadOnlyRepository<Contact> contactReadOnlyRepository,
        IReadOnlyRepository<Account> accountReadOnlyRepository)
    {
        _contactReadOnlyRepository = contactReadOnlyRepository;
        _accountReadOnlyRepository = accountReadOnlyRepository;
    }

    public async Task<Contact> CreateAsync(string name, string email, string? phone, Guid accountId)
    {
        await CheckValidOnCreateAsync(email, accountId);

        return new Contact(name, email, phone, accountId);
    }

    public void AddAccount(Contact contact, Account account)
    {
        contact.Account = account;
    }

    private async Task CheckValidOnCreateAsync(string email, Guid accountId)
    {
        await CheckEmailDuplicationAsync(email);

        await CheckAccountIdExist(accountId);
    }

    private async Task CheckAccountIdExist(Guid accountId)
    {
        var accountIdSpecification = new AccountIdSpecification(accountId);

        Optional<Account>.Of(await _accountReadOnlyRepository.GetAnyAsync(accountIdSpecification))
            .ThrowIfNotPresent(new AccountNotFoundException(accountId));
    }

    private async Task CheckEmailDuplicationAsync(string email)
    {
        var contactEmailExactMatchSpecification = new ContactEmailExactMatchSpecification(email);

        Optional<bool>.Of(await _contactReadOnlyRepository.CheckIfExistAsync(contactEmailExactMatchSpecification))
            .ThrowIfPresent(new ContactDuplicatedException(nameof(email), email));
    }
}