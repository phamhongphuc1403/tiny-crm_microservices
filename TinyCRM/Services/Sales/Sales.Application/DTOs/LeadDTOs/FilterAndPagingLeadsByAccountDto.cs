using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.LeadDTOs.Enums;

namespace Sales.Application.DTOs.LeadDTOs;

public class FilterAndPagingLeadsByAccountDto : FilterAndPagingDto<LeadSortProperty>
{
    public Guid AccountId { get; set; }
}