using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.ProductAggregate.Repositories;

public interface IProductReadOnlyRepository : IReadOnlyRepository<Product>
{
    new Task<List<Product>> GetAllAsync(ISpecification<Product>? specification = null, string? includeTables = null);
}