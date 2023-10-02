using Sales.Application.DTOs.ProductDTOs;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Infrastructure.EFCore.Mapper;

public class ProductMapper : Mapper
{
    public ProductMapper()
    {
        CreateMap<Product, ProductSummaryDto>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Math.Round(src.Price, 2)));
    }
}