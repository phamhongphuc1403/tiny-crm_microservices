using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.DTOs.Leads;

public class LeadSummaryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public LeadStatus Status { get; set; }
}