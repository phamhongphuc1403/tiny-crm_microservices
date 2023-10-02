using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.ProductDTOs;

namespace Sales.Application.CQRS.Queries.ProductQueries.Requests;

public class FilterAndPagingProductsQuery : FilterAndPagingProductsDto,
    IQuery<FilterAndPagingResultDto<ProductSummaryDto>>
{
    public FilterAndPagingProductsQuery(FilterAndPagingProductsDto dto)
    {
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        Type = dto.Type;
    }

    public string Sort { get; private init; }
}