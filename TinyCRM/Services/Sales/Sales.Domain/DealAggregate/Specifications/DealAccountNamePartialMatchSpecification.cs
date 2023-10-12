using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Specifications;

public class DealAccountNamePartialMatchSpecification : Specification<Deal>
{
    private readonly string _keyword;

    public DealAccountNamePartialMatchSpecification(string keyword)
    {
        _keyword = keyword;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_keyword)) return lead => true;

        return deal => deal.Customer.Name.ToUpper().Contains(_keyword.ToUpper());
    }
}