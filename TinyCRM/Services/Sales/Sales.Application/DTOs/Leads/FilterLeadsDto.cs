using BuildingBlock.Application.DTOs;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.DTOs.Leads;

public class FilterLeadsDto : FilterDto
{
    
    public LeadStatus Status { get; set; }
}