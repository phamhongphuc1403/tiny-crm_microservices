using BuildingBlock.Domain.Model;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.DealAggregate.Entities;

public class DealLine : GuidEntity
{
    public DealLine(Guid productId, double pricePerUnit, int quantity)
    {
        ProductId = productId;
        PricePerUnit = pricePerUnit;
        Quantity = quantity;
        TotalAmount = pricePerUnit * quantity;
        CreatedDate = DateTime.UtcNow;
    }

    public Guid DealId { get; set; }
    public Deal Deal { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public double PricePerUnit { get; set; }
    public int Quantity { get; set; }
    public double TotalAmount { get; set; }
}