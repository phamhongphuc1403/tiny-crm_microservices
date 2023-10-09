using BuildingBlock.Domain.Exceptions;

namespace Sales.Domain.DealAggregate.Exceptions;

public class DealLineNotFoundException : EntityNotFoundException
{
    public DealLineNotFoundException(Guid id) : base(nameof(DealLine), id)
    {
    }
}