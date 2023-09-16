using BuildingBlock.Domain.Model;
using Sales.Domain.Entities.Enums;

namespace Sales.Domain.Entities;

public class Lead : GuidEntity
{
    public string Title { get; set; } = null!;
    public Guid CustomerId { get; set; }
    // public virtual AccountEntity Customer { get; set; } = null!;
    public LeadSource? Source { get; set; }
    public double? EstimatedRevenue { get; set; }
    public string? Description { get; set; }
    
    public LeadStatus Status { get; set; }
    public LeadDisqualificationReason? DisqualificationReason { get; set; }
    public string? DisqualificationDescription { get; set; }
    public DateTime DisqualificationDate { get; set; }
    // public virtual Deal? Deal { get; set; }

    private Lead(string title, Guid customerId, LeadSource? source, double? estimatedRevenue,
        string? description)
    {
        Title = title;
        CustomerId = customerId;
        Source = source;
        EstimatedRevenue = estimatedRevenue;
        Description = description;
        Status = LeadStatus.Prospect;
    }

    public Lead()
    {
        
    }

    public static Lead Create(string title, Guid customerId, LeadSource? source, double? estimatedRevenue,
        string? description)
    {
        return new Lead(title, customerId, source, estimatedRevenue, description);
    }
}