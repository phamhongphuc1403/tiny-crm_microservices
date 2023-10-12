using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Queries.DealQueries.Requests;

public class FilterAndPagingDealLinesQuery : FilterAndPagingDealLineDto, IQuery<FilterAndPagingResultDto<DealLineDto>>
{
    public FilterAndPagingDealLinesQuery(Guid dealId, FilterAndPagingDealLineDto dto)
    {
        DealId = dealId;
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
    }

    public Guid DealId { get; }
    public string Sort { get; }
}