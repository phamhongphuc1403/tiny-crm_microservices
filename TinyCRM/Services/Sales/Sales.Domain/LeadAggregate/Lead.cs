using BuildingBlock.Domain.Model;
using Sales.Domain.AccountAggregate;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Domain.LeadAggregate;

public sealed class Lead : GuidEntity
{
    internal Lead(string title, Guid customerId, LeadSource? source, double? estimatedRevenue,
        string? description)
    {
        Title = title;
        CustomerId = customerId;
        Source = source;
        EstimatedRevenue = estimatedRevenue ?? 0;
        Description = description;
        Status = LeadStatus.Prospect;
    }

    public Lead()
    {
    }

    public string Title { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public Account Customer { get; set; } = null!;
    public LeadSource? Source { get; set; }
    public double EstimatedRevenue { get; set; }
    public string? Description { get; set; }

    public LeadStatus Status { get; set; }
    public DateTime? QualificationDate { get; set; }
    public LeadDisqualificationReason? DisqualificationReason { get; set; }
    public string? DisqualificationDescription { get; set; }
    public DateTime? DisqualificationDate { get; set; }

    public Deal? Deal { get; set; }

    internal void Update(string title, Guid customerId, LeadSource? source, double? estimatedRevenue,
        string? description, LeadStatus status)
    {
        Title = title;
        CustomerId = customerId;
        Source = source;
        EstimatedRevenue = estimatedRevenue ?? 0;
        Description = description;
        Status = status;
    }
}