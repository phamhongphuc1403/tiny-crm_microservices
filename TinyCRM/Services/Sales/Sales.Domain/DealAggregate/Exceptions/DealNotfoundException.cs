using BuildingBlock.Domain.Exceptions;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Exceptions;

public class DealNotfoundException : EntityNotFoundException
{
    public DealNotfoundException(Guid id) : base(nameof(Deal), id)
    {
    }

    public DealNotfoundException(string message) : base(message)
    {
    }
}