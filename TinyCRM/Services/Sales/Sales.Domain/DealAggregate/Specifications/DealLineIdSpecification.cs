using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Specifications;

public class DealLineIdSpecification : Specification<DealLine>
{
    private readonly Guid _id;

    public DealLineIdSpecification(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<DealLine, bool>> ToExpression()
    {
        return dealLine => dealLine.Id == _id;
    }
}