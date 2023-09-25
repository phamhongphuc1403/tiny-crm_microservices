using Sales.Application.DTOs.Leads;
using Sales.Domain.LeadAggregate;

namespace Sales.Infrastructure.EFCore.Mapper;

public class LeadMapper : Mapper
{
    public LeadMapper()
    {
        CreateMap<Lead, LeadDto>();
    }
}