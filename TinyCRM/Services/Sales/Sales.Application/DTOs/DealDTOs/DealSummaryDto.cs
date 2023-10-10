using Sales.Domain.DealAggregate.Enums;

namespace Sales.Application.DTOs.DealDTOs;

public class DealSummaryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public DealStatus Status { get; set; }
}