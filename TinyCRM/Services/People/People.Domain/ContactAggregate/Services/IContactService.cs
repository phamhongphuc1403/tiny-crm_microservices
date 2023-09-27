using People.Domain.AccountAggregate.Entities;
using People.Domain.ContactAggregate.Entities;

namespace People.Domain.ContactAggregate.Services;

public interface IContactService
{
    Task<Contact> CreateAsync(string name, string email, string? phone, Guid accountId);
    void AddAccount(Contact contact, Account account);
}