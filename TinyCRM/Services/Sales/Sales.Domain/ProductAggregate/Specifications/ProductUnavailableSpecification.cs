using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.ProductAggregate.Specifications;

public class ProductUnavailableSpecification : Specification<Product>
{
    private readonly bool _isUnavailable;

    public ProductUnavailableSpecification(bool isUnavailable)
    {
        _isUnavailable = isUnavailable;
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        if (_isUnavailable) return product => true;

        return product => product.IsAvailable == true;
    }
}