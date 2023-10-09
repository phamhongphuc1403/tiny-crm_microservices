using BuildingBlock.Domain.Exceptions;

namespace Sales.Domain.DealAggregate.Exceptions;

public class DealNotfoundException : EntityNotFoundException
{
    public DealNotfoundException(Guid id) : base(nameof(Deal), id)
    {
    }
}