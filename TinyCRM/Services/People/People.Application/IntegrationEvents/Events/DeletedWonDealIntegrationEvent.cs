using BuildingBlock.Application.IntegrationEvents.Events;

namespace People.Application.IntegrationEvents.Events;

public record DeletedWonDealIntegrationEvent: IntegrationEvent
{
    public Guid AccountId { get; private set; }
    public double ActualRevenue { get; private set; }

    public DeletedWonDealIntegrationEvent(Guid accountId, double actualRevenue)
    {
        AccountId = accountId;
        ActualRevenue = actualRevenue;
    }
}