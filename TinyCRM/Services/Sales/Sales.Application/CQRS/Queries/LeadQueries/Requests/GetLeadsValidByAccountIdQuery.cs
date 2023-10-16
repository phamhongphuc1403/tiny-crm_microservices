using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.LeadDTOs;
using Sales.Application.DTOs.LeadDTOs.Enums;

namespace Sales.Application.CQRS.Queries.LeadQueries.Requests;

public class GetLeadsValidByAccountIdQuery : FilterAndPagingDto<LeadSortProperty>,
    IQuery<FilterAndPagingResultDto<LeadSummaryDto>>
{
    public string Sort { get; }
    public Guid AccountId { get; }


    public GetLeadsValidByAccountIdQuery(FilterAndPagingDto<LeadSortProperty> dto, Guid accountId)
    {
        AccountId = accountId;
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        if (dto.SortBy == LeadSortProperty.CustomerName)
            Sort = Sort.Replace(nameof(LeadSortProperty.CustomerName), "Customer.Name");
    }
}