using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Specifications;

public class DealLineProductCodePartialMatchSpecification : Specification<DealLine>
{
    private readonly string _code;

    public DealLineProductCodePartialMatchSpecification(string code)
    {
        _code = code;
    }

    public override Expression<Func<DealLine, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_code)) return dealLine => true;

        return dealLine => dealLine.Product.Code.ToUpper().Contains(_code.ToUpper());
    }
}