using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.Enums;
using Sales.Domain.Entities.Enums;

namespace Sales.Application.DTOs;

public class FilterAndPagingLeadsDto : FilterAndPagingDto<LeadSortProperty>
{
    public LeadStatus? Status { get; set; }
}