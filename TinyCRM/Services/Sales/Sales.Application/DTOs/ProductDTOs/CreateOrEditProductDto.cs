using Sales.Domain.ProductAggregate.Entities.Enums;

namespace Sales.Application.DTOs.ProductDTOs;

public class CreateOrEditProductDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ProductType Type { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
}