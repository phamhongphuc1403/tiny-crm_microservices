using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.Entities.Enums;
using Sales.Domain.ProductAggregate.Exceptions;
using Sales.Domain.ProductAggregate.Specifications;

namespace Sales.Domain.ProductAggregate.DomainService;

public class ProductDomainService : IProductDomainService
{
    private readonly IReadOnlyRepository<Product> _readOnlyRepository;

    public ProductDomainService(IReadOnlyRepository<Product> readOnlyRepository)
    {
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<Product> CreateAsync(string code, string name, double price, bool isAvailable, ProductType type)
    {
        var productCodeSpecification = new ProductCodeExactMatchSpecification(code);

        Optional<bool>.Of(await _readOnlyRepository.CheckIfExistAsync(productCodeSpecification))
            .ThrowIfPresent(new ProductConflictExceptionException(nameof(code), code));

        return new Product(code, name, price, isAvailable, type);
    }
}