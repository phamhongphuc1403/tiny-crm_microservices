using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.DealDTOs;
using Sales.Application.DTOs.DealDTOs.Enums;

namespace Sales.Application.CQRS.Queries.DealQueries.Requests;

public class GetDealsByAccountIdQuery : FilterAndPagingDealsDto, IQuery<FilterAndPagingResultDto<DealSummaryDto>>
{
    public GetDealsByAccountIdQuery(FilterAndPagingDealsDto dto, Guid accountId)
    {
        AccountId = accountId;
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        if (dto.SortBy == DealSortProperty.CustomerName)
            Sort = Sort.Replace(nameof(DealSortProperty.CustomerName), "Customer.Name");
        Status = dto.Status;
    }

    public string Sort { get; }
    public Guid AccountId { get; }
}