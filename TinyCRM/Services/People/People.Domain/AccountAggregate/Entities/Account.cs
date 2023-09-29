using BuildingBlock.Domain.Model;
using People.Domain.ContactAggregate.Entities;

namespace People.Domain.AccountAggregate.Entities;

public sealed class Account : AggregateRoot
{
    public Account(string name, string? email, string? phone, string? address)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
    }
    
    public Account(Guid id, string name, string? email, string? phone, string? address)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        Id = id;
    }

    public Account()
    {
    }

    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public double TotalSales { get; set; }

    public ICollection<Contact> Contacts { get; private set; } = null!;
}