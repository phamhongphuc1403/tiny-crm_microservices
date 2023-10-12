using BuildingBlock.Application.DTOs;
using Sales.Domain.DealAggregate.Enums;

namespace Sales.Application.DTOs.DealDTOs;

public class FilterDealsDto:FilterDto
{
    public DealStatus? Status { get; set; }
}