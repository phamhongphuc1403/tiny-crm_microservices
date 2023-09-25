using BuildingBlock.Domain.Exceptions;

namespace Sales.Domain.LeadAggregate.Exceptions;

public class LeadNotFoundException : EntityNotFoundException
{
    public LeadNotFoundException(Guid id) : base(nameof(Lead), id)
    {
    }
}