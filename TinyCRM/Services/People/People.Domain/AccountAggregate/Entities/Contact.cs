using BuildingBlock.Domain.Model;

namespace People.Domain.AccountAggregate.Entities;

public class Contact : GuidEntity
{
    public string Name { get; private set; } = null!;
    public string? Email { get; }
    public string? Phone { get; }

    public Guid AccountId { get; }
    public Account Account { get; private set; } = null!;
}