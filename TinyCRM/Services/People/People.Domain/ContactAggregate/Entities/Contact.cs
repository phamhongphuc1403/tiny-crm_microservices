using BuildingBlock.Domain.Model;
using People.Domain.AccountAggregate.Entities;

namespace People.Domain.ContactAggregate.Entities;

public class Contact : GuidEntity
{
    public Contact()
    {
    }

    public Contact(string name, string email, string? phone, Guid accountId)
    {
        Name = name;
        Email = email;
        Phone = phone;
        AccountId = accountId;
    }

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }

    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;

    public void Edit(string name, string email, string? phone, Guid accountId)
    {
        Name = name;
        Email = email;
        Phone = phone;
        AccountId = accountId;
    }
}