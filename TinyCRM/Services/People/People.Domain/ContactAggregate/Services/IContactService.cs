using People.Domain.ContactAggregate.Entities;

namespace People.Domain.ContactAggregate.Services;

public interface IContactService
{
    Task<Contact> CreateAsync(string name, string email, string? phone, Guid accountId);
    Task AddAccount(Contact contact, Guid accountId);
    Task<Contact> EditAsync(Guid id, string name, string email, string? phone, Guid accountId);
    Task<IEnumerable<Contact>> DeleteManyAsync(IEnumerable<Guid> ids);
}