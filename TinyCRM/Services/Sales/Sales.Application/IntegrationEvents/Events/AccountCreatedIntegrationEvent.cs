using BuildingBlock.Application.IntegrationEvents.Events;

namespace Sales.Application.IntegrationEvents.Events;

public record AccountCreatedIntegrationEvent(Guid AccountId, string? Email, string Name) : IntegrationEvent
{
    public Guid AccountId { get; } = AccountId;
    public string? Email { get; } = Email;
    public string Name { get; } = Name;
}