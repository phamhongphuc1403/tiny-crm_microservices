using Sales.Application.DTOs.ProductDTOs;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Infrastructure.EFCore.Mapper;

public class ProductMapper : Mapper
{
    public ProductMapper()
    {
        CreateMap<Product, ProductSummaryDto>();
        CreateMap<Product, ProductDetailDto>();
    }
}