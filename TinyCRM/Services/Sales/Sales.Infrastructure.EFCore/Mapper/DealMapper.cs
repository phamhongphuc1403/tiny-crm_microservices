using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate;

namespace Sales.Infrastructure.EFCore.Mapper;

public class DealMapper : Mapper
{
    public DealMapper()
    {
        CreateMap<Deal, DealSummaryDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
    }
}