using BuildingBlock.Application.IntegrationEvents.Events;

namespace Sales.Application.IntegrationEvents.Events;

public record AccountEditedIntegrationEvent : IntegrationEvent
{
    public Guid AccountId { get; }
    public string Name { get; private set; } = null!;
    public string? Email { get; }
}