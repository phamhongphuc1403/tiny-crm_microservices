using BuildingBlock.Domain.Exceptions;
using Sales.Domain.Entities;

namespace Sales.Domain.Exceptions;

public class LeadNotFoundException : EntityNotFoundException
{
    public LeadNotFoundException(Guid id) : base(nameof(Lead), id)
    {
    }
}