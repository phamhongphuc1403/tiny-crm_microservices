using BuildingBlock.Domain.DomainEvent;

namespace Sales.Domain.DealAggregate.Events;

public class CreatedDealEvent : IDomainEvent
{
    public Guid LeadId { get; set; }

    public CreatedDealEvent(Guid leadId)
    {
        LeadId = leadId;
    }

    public CreatedDealEvent()
    {
    }
}