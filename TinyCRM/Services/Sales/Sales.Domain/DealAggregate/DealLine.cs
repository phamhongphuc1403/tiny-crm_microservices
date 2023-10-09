using BuildingBlock.Domain.Model;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.DealAggregate;

public class DealLine : GuidEntity
{
    public Guid DealId { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string Code { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public double TotalAmount { get; set; }

    public DealLine(Guid dealId, Guid productId, string code, double price, int quantity, double totalAmount)
    {
        DealId = dealId;
        ProductId = productId;
        Code = code;
        Price = price;
        Quantity = quantity;
        TotalAmount = totalAmount;
    }
}