using BuildingBlock.Application.IntegrationEvents.Events;

namespace Sales.Application.IntegrationEvents.Events;

public record AccountCreatedIntegrationEvent : IntegrationEvent
{
    public Guid AccountId { get; init; }
    public string Name { get; init; } = null!;
    public string? Email { get; init; }

    public AccountCreatedIntegrationEvent(Guid accountId, string name, string? email)
    {
        AccountId = accountId;
        Name = name;
        Email = email;
    }
}