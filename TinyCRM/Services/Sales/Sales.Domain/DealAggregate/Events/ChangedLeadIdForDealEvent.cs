using BuildingBlock.Domain.DomainEvent;

namespace Sales.Domain.DealAggregate.Events;

public class ChangedLeadIdForDealEvent:IDomainEvent
{
    public Guid? LeadOldId { get; set; }
    public Guid LeadNewId { get; set; }
    public ChangedLeadIdForDealEvent(Guid? leadOldId, Guid leadNewId)
    {
        LeadOldId = leadOldId;
        LeadNewId = leadNewId;
    }
}