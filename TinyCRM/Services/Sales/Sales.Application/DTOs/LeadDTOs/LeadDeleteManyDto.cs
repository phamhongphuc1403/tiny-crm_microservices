namespace Sales.Application.DTOs.LeadDTOs;

public class LeadDeleteManyDto
{
    public IEnumerable<Guid> Ids { get; set; } = null!;
}