using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Specifications;

public class DealAccountIdSpecification: Specification<Deal>
{
    private readonly Guid _accountId;

    public DealAccountIdSpecification(Guid accountId)
    {
        _accountId = accountId;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return deal => deal.CustomerId == _accountId;
    }
}