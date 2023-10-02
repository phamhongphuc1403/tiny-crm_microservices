using BuildingBlock.Domain.Model;
using Sales.Domain.ProductAggregate.Entities.Enums;

namespace Sales.Domain.ProductAggregate.Entities;

public sealed class Product : GuidEntity
{
    private Product(string code, string name, double price, bool isAvailable, ProductType type)
    {
        Code = code;
        Name = name;
        Price = price;
        IsAvailable = isAvailable;
        Type = type;
    }

    public Product()
    {
    }

    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public double Price { get; private set; }
    public bool IsAvailable { get; private set; }
    public ProductType Type { get; private set; }
}