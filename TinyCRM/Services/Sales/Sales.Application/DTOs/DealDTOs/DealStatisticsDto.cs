using BuildingBlock.Application.DTOs;

namespace Sales.Application.DTOs.DealDTOs;

public class DealStatisticsDto
{
    public List<CardDto> Cards { get; set; } = null!;
}