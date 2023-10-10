using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.DealDTOs;
using Sales.Application.DTOs.DealDTOs.Enums;

namespace Sales.Application.CQRS.Queries.DealQueries.Requests;

public class FilterAndPagingDealsQuery: FilterAndPagingDealsDto, IQuery<FilterAndPagingResultDto<DealSummaryDto>>
{
    public FilterAndPagingDealsQuery(FilterAndPagingDealsDto dto)
    {
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        if (dto.SortBy == DealSortProperty.Customer)
            Sort = Sort.Replace("Customer", "Customer.Name");
        Status = dto.Status;
    }
    public string Sort { get; private init; }
}