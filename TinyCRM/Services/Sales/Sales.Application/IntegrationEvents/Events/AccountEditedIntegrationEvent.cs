using BuildingBlock.Application.IntegrationEvents.Events;

namespace Sales.Application.IntegrationEvents.Events;

public record AccountEditedIntegrationEvent : IntegrationEvent
{
    public Guid AccountId { get; private set; }
    public string? Email { get; private set; }
    public string Name { get; private set; }

    public AccountEditedIntegrationEvent(Guid accountId, string? email, string name)
    {
        AccountId = accountId;
        Email = email;
        Name = name;
    }
}