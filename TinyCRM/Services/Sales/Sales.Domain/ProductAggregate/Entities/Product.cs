using BuildingBlock.Domain.Model;
using Sales.Domain.ProductAggregate.Entities.Enums;

namespace Sales.Domain.ProductAggregate.Entities;

public sealed class Product : GuidEntity
{
    public Product(string code, string name, double price, bool isAvailable, ProductType type)
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

    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
    public ProductType Type { get; set; }
}