using BuildingBlock.Domain.Exceptions;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Exceptions;

public class DealLineNotFoundException : EntityNotFoundException
{
    public DealLineNotFoundException(Guid id) : base(nameof(DealLine), id)
    {
    }
}