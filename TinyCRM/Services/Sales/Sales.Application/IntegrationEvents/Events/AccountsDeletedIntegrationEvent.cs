using BuildingBlock.Application.IntegrationEvents.Events;

namespace Sales.Application.IntegrationEvents.Events;

public record AccountsDeletedIntegrationEvent : IntegrationEvent
{
    public AccountsDeletedIntegrationEvent(IEnumerable<Guid> accountIds)
    {
        AccountIds = accountIds;
    }

    public IEnumerable<Guid> AccountIds { get; private set; } = null!;
}