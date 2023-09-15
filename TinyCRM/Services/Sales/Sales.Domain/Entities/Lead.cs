using BuildingBlock.Domain.Model;
using Sales.Domain.Entities.Enums;

namespace Sales.Domain.Entities;

public class Lead : GuidEntity
{
    public string Title { get; private set; } = null!;

    public Guid CustomerId { get; }

    // public virtual AccountEntity Customer { get; private set; } = null!;
    public LeadSources? Source { get; }
    public double? EstimatedRevenue { get; }
    public string? Description { get; }
    public LeadStatuses? Status { get; }
    public LeadDisqualificationReasons? DisqualificationReason { get; }
    public string? DisqualificationDescription { get; }

    public DateTime DisqualificationDate { get; }
    // public virtual Deal? Deal { get; private set; }
}