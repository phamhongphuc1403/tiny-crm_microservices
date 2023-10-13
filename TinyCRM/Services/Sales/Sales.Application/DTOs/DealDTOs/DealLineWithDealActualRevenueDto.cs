namespace Sales.Application.DTOs.DealDTOs;

public class DealLineWithDealActualRevenueDto
{
    public DealLineDto DealLine { get; set; } = null!;
    public DealActualRevenueDto Deal { get; set; } = null!;
}