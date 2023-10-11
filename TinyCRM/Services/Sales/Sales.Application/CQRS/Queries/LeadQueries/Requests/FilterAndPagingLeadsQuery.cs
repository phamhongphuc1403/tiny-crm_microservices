using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.LeadDTOs;
using Sales.Application.DTOs.LeadDTOs.Enums;

namespace Sales.Application.CQRS.Queries.LeadQueries.Requests;

public sealed class FilterAndPagingLeadsQuery : FilterAndPagingLeadsDto,
    IQuery<FilterAndPagingResultDto<LeadSummaryDto>>
{
    public FilterAndPagingLeadsQuery(FilterAndPagingLeadsDto dto)
    {
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        if (dto.SortBy == LeadSortProperty.Customer)
            Sort = Sort.Replace("Customer", "Customer.Name");
        Status = dto.Status;
    }

    public string Sort { get; }
}