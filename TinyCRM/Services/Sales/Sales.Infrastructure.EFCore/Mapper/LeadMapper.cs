using Sales.Application.DTOs.LeadDTOs;
using Sales.Domain.LeadAggregate;

namespace Sales.Infrastructure.EFCore.Mapper;

public class LeadMapper : Mapper
{
    public LeadMapper()
    {
        CreateMap<Lead, LeadDetailDto>();
        CreateMap<Lead, LeadSummaryDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
    }
}