using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.LeadDTOs.Enums;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.DTOs.LeadDTOs;

public class FilterAndPagingLeadsDto : FilterAndPagingDto<LeadSortProperty>
{
    public LeadStatus? Status { get; set; }
}