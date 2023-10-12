namespace Sales.Application.DTOs.DealDTOs;

public class DealDeleteManyDto
{
    public IEnumerable<Guid> Ids { get; set; } = null!;
}