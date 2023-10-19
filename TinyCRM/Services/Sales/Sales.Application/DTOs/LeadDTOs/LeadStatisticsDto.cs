using BuildingBlock.Application.DTOs;

namespace Sales.Application.DTOs.LeadDTOs;

public class LeadStatisticsDto
{
    public List<CardDto> Cards { get; set; } = null!;
}