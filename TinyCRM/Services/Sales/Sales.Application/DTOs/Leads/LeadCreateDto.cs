using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.DTOs.Leads;

public class LeadCreateDto
{
    public string Title { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public LeadSource? Source { get; set; }
    public double? EstimatedRevenue { get; set; }
    public string? Description { get; set; }
}