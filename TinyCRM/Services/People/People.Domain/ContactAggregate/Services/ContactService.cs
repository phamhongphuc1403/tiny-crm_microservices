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

    public async Task AddAccount(Contact contact, Guid accountId)
    {
        var accountIdSpecification = new AccountIdSpecification(accountId);

        var account = Optional<Account>.Of(await _accountReadOnlyRepository.GetAnyAsync(accountIdSpecification))
            .ThrowIfNotPresent(new AccountNotFoundException(accountId)).Get();

        contact.Account = account;
    }

    public async Task<Contact> EditAsync(Guid id, string name, string email, string? phone, Guid accountId)
    {
        var contact = await CheckValidOnEditAsync(id, name, email, accountId);

        contact.Edit(name, email, phone, accountId);

        return contact;
    }

    private async Task<Contact> CheckValidOnEditAsync(Guid id, string name, string email, Guid accountId)
    {
        var contact = await CheckContactExist(id);

        await CheckEmailDuplicationAsync(email, id);

        await CheckAccountIdExist(accountId);

        return contact;
    }

    private async Task<Contact> CheckContactExist(Guid id)
    {
        var contactIdSpecification = new ContactIdSpecification(id);

        return Optional<Contact>.Of(await _contactReadOnlyRepository.GetAnyAsync(contactIdSpecification))
            .ThrowIfNotPresent(new ContactNotFoundException(id)).Get();
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

    private async Task CheckEmailDuplicationAsync(string email, Guid id)
    {
        var contactEmailExactMatchSpecification = new ContactEmailExactMatchSpecification(email);

        var contactIdNotEqualSpecification = new ContactIdNotEqualSpecification(id);

        var specification = contactEmailExactMatchSpecification.And(contactIdNotEqualSpecification);

        Optional<bool>.Of(await _contactReadOnlyRepository.CheckIfExistAsync(specification))
            .ThrowIfPresent(new ContactDuplicatedException(nameof(email), email));
    }
}