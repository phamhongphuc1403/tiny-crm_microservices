using BuildingBlock.Application.IntegrationEvents.Events;

namespace People.Application.IntegrationEvents.Events;

public record ClosedWonDealIntegrationEvent : IntegrationEvent
{
    public Guid AccountId { get; private set; }
    public double ActualRevenue { get; private set; }

    public ClosedWonDealIntegrationEvent(Guid accountId, double actualRevenue)
    {
        AccountId = accountId;
        ActualRevenue = actualRevenue;
    }
}