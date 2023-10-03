using BuildingBlock.Domain.Exceptions;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.ProductAggregate.Exceptions;

public class ProductNotFoundException : EntityNotFoundException
{
    public ProductNotFoundException(Guid id) : base(nameof(Product), id)
    {
    }
}