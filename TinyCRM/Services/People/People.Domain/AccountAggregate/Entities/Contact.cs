using BuildingBlock.Domain.Model;

namespace People.Domain.AccountAggregate.Entities;

public class Contact : GuidEntity
{
    public string Name { get; private set; } = null!;
    public string? Email { get; private set; }
    public string? Phone { get; private set; }

    public Guid AccountId { get; private set; }
    public Account Account { get; private set; } = null!;

    public Contact()
    {
        
    }
}