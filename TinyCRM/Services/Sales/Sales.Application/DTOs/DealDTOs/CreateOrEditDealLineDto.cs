namespace Sales.Application.DTOs.DealDTOs;

public class CreateOrEditDealLineDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public double PricePerUnit { get; set; }
}