using BuildingBlock.Domain.Model;

namespace Sales.Domain.AccountAggregate;

public sealed class Account : AggregateRoot
{
    private Account()
    {
    }


    internal Account(string name, string? email = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }

    internal Account(Guid id, string name, string? email = null)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Name = name;
        Email = email;
    }

    public string Name { get; private set; } = null!;
    public string? Email { get; private set; }

    internal void Update(string name, string? email = null)
    {
        Name = name;
        Email = email;
    }
}