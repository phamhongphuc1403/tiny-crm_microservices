using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Infrastructure.EFCore.Mapper;

public class DealMapper : Mapper
{
    public DealMapper()
    {
        CreateMap<Deal, DealSummaryDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
        CreateMap<Deal, DealDetailDto>();

        CreateMap<DealLine, DealLineDto>()
            .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.Product.Code))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));

        CreateMap<Deal, DealActualRevenueDto>()
            .ForMember(dest => dest.ActualRevenue, opt => opt.MapFrom(src => src.ActualRevenue));

        CreateMap<DealLine, DealLineWithDealActualRevenueDto>()
            .ForMember(dest => dest.Deal, opt => opt.MapFrom(src => src.Deal))
            .ForMember(dest => dest.DealLine, opt => opt.MapFrom(src => src));
    }
}