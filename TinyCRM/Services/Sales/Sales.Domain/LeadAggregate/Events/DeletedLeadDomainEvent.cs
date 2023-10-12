using BuildingBlock.Domain.DomainEvent;

namespace Sales.Domain.LeadAggregate.Events;

public class DeletedLeadDomainEvent:IDomainEvent
{
    public Guid LeadId { get; set; }

    public DeletedLeadDomainEvent(Guid leadId)
    {
        LeadId = leadId;
    }

    public DeletedLeadDomainEvent()
    {
    }
}