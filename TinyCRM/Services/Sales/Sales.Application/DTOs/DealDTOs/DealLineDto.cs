namespace Sales.Application.DTOs.DealDTOs;

public class DealLineDto
{
    public Guid Id { get; set; }
    public Guid DealId { get; set; }
    public string ProductCode { get; set; } = null!;
    public double PricePerUnit { get; set; }
    public int Quantity { get; set; }
    public double TotalAmount { get; set; }
}