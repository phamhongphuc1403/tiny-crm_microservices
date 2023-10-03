using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Application.CQRS.Queries.ProductQueries.Requests;
using Sales.Application.DTOs.ProductDTOs;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.Exceptions;
using Sales.Domain.ProductAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.ProductQueries.Handlers;

public class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Product> _repository;

    public GetProductQueryHandler(
        IReadOnlyRepository<Product> repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDetailDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productIdSpecification = new ProductIdSpecification(request.Id);

        var product = Optional<Product>.Of(await _repository.GetAnyAsync(productIdSpecification))
            .ThrowIfNotPresent(new ProductNotFoundException(request.Id)).Get();

        return _mapper.Map<ProductDetailDto>(product);
    }
}