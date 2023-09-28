using BuildingBlock.Application.IntegrationEvents.Events;

namespace People.Application.IntegrationEvents.Events;

public record AccountsDeletedIntegrationEvent(IEnumerable<Guid> AccountIds) : IntegrationEvent
{
    public IEnumerable<Guid> AccountIds = AccountIds;
}