using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Specifications;

public class DealLineDealIdSpecification : Specification<DealLine>
{
    private readonly Guid _dealId;

    public DealLineDealIdSpecification(Guid dealId)
    {
        _dealId = dealId;
    }

    public override Expression<Func<DealLine, bool>> ToExpression()
    {
        return dealLine => dealLine.Deal.Id == _dealId;
    }
}