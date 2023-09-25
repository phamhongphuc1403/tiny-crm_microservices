using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.Leads.Enums;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.DTOs.Leads;

public class FilterAndPagingLeadsDto : FilterAndPagingDto<LeadSortProperty>
{
    public LeadStatus? Status { get; set; }
}