using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.ProductDTOs.Enums;
using Sales.Domain.ProductAggregate.Entities.Enums;

namespace Sales.Application.DTOs.ProductDTOs;

public class FilterAndPagingProductsDto : FilterAndPagingDto<ProductSortProperty>
{
    public ProductType? Type { get; set; }
}