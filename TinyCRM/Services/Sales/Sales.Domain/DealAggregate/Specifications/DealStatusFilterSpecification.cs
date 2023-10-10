using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.DealAggregate.Enums;

namespace Sales.Domain.DealAggregate.Specifications;

public class DealStatusFilterSpecification : Specification<Deal>
{
    private readonly DealStatus? _status;

    public DealStatusFilterSpecification(DealStatus? status)
    {
        _status = status;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        if (_status == null) return deal => true;
        return deal => deal.DealStatus == _status;
    }
}