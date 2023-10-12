namespace Sales.Application.DTOs.DealDTOs;

public class DeleteManyDealLinesDto
{
    public IEnumerable<Guid> DealLineIds { get; set; } = null!;
}