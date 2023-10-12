using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Specifications;

public class DealIdSpecification : Specification<Deal>
{
    private readonly Guid _id;

    public DealIdSpecification(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return deal => deal.Id == _id;
    }
}