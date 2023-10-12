using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.LeadDTOs;
using Sales.Application.DTOs.LeadDTOs.Enums;

namespace Sales.Application.CQRS.Queries.LeadQueries.Requests;

public class GetLeadsByAccountIdQuery : FilterAndPagingLeadsByAccountDto, IQuery<FilterAndPagingResultDto<LeadSummaryDto>>
{
    public string Sort { get; }

    public GetLeadsByAccountIdQuery(FilterAndPagingLeadsByAccountDto dto)
    {
        AccountId = dto.AccountId;
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        if (dto.SortBy == LeadSortProperty.Customer)
            Sort = Sort.Replace("Customer", "Customer.Name");
    }
    public GetLeadsByAccountIdQuery(FilterAndPagingDto<LeadSortProperty> dto, Guid accountId)
    {
        AccountId = accountId;
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        if (dto.SortBy == LeadSortProperty.Customer)
            Sort = Sort.Replace("Customer", "Customer.Name");
    }
}