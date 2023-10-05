using BuildingBlock.Application.DTOs;
using Sales.Domain.ProductAggregate.Entities.Enums;

namespace Sales.Application.DTOs.ProductDTOs;

public class FilterProductsDto : FilterDto
{
    public ProductType? Type { get; set; }
}