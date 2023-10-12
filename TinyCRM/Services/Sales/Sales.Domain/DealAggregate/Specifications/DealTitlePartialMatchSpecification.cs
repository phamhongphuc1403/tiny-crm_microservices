using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Specifications;

public class DealTitlePartialMatchSpecification : Specification<Deal>
{
    public DealTitlePartialMatchSpecification(string keyword)
    {
        Keyword = keyword;
    }

    private string Keyword { get; }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return deal => deal.Title.ToUpper().Contains(Keyword.ToUpper());
    }
}