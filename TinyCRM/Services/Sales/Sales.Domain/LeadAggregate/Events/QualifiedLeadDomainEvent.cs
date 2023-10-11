using BuildingBlock.Domain.DomainEvent;

namespace Sales.Domain.LeadAggregate.Events;

public class QualifiedLeadDomainEvent : IDomainEvent
{
    public QualifiedLeadDomainEvent()
    {
    }

    public QualifiedLeadDomainEvent(Guid dealId, Guid leadId, Guid customerId, double estimatedRevenue, string title)
    {
        DealId = dealId;
        LeadId = leadId;
        CustomerId = customerId;
        EstimatedRevenue = estimatedRevenue;
        Title = title;
    }

    public Guid DealId { get; set; }
    public Guid LeadId { get; set; }
    public Guid CustomerId { get; set; }
    public double EstimatedRevenue { get; set; }
    public string Title { get; set; } = null!;
}