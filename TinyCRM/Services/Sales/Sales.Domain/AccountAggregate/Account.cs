using BuildingBlock.Domain.Model;

namespace Sales.Domain.AccountAggregate;

public sealed class Account : AggregateRoot
{
    public string Name { get; private set; } = null!;
    public string? Email { get; private set; }

    private Account()
    {
    }


    internal Account(string name, string? email = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }
}