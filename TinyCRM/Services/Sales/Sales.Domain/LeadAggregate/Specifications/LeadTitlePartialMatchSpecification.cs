using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;

namespace Sales.Domain.LeadAggregate.Specifications;

public class LeadTitlePartialMatchSpecification : Specification<Lead>
{
    private readonly string _keyword;

    public LeadTitlePartialMatchSpecification(string keyword)
    {
        _keyword = keyword;
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_keyword)) return lead => true;

        return lead => lead.Title.Contains(_keyword);
    }
}