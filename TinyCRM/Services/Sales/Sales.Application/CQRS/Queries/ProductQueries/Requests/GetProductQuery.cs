using BuildingBlock.Application.CQRS.Query;
using Sales.Application.DTOs.ProductDTOs;

namespace Sales.Application.CQRS.Queries.ProductQueries.Requests;

public class GetProductQuery : IQuery<ProductDetailDto>
{
    public GetProductQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}