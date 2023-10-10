using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.DealDTOs.Enums;
using Sales.Domain.DealAggregate.Enums;

namespace Sales.Application.DTOs.DealDTOs;

public class FilterAndPagingDealsDto:FilterAndPagingDto<DealSortProperty>
{
    public DealStatus Status { get; set; }
}