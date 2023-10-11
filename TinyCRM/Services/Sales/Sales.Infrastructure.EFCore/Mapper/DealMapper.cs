using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate;

namespace Sales.Infrastructure.EFCore.Mapper;

public class DealMapper : Mapper
{
    public DealMapper()
    {
        CreateMap<Deal, DealSummaryDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
        CreateMap<Deal, DealDetailDto>();

        CreateMap<DealLine, DealLineDto>()
            .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.Product.Code));
    }
}