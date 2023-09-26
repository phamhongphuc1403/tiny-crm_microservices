using BuildingBlock.Application.IntegrationEvents.Events;

namespace People.Application.IntegrationEvents.Events;

public record AccountDeletedIntegrationEvent : IntegrationEvent
{
    public Guid AccountId;

    public AccountDeletedIntegrationEvent(Guid id)
    {
        AccountId = id;
    }
}