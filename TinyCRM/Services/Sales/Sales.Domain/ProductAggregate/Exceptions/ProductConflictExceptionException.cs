using BuildingBlock.Domain.Exceptions;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.ProductAggregate.Exceptions;

public class ProductConflictExceptionException : EntityDuplicatedException
{
    public ProductConflictExceptionException(string column, string value) : base(nameof(Product), column, value)
    {
    }
}