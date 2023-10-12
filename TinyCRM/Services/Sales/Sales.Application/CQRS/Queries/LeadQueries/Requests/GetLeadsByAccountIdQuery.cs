using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.LeadDTOs;
using Sales.Application.DTOs.LeadDTOs.Enums;

namespace Sales.Application.CQRS.Queries.LeadQueries.Requests;

public class GetLeadsByAccountIdQuery : FilterAndPagingLeadsDto, IQuery<FilterAndPagingResultDto<LeadSummaryDto>>
{
    public Guid AccountId { get; private set; }
    public string Sort { get; }

    public GetLeadsByAccountIdQuery(Guid accountId, FilterAndPagingLeadsDto dto)
    {
        AccountId = accountId;
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        if (dto.SortBy == LeadSortProperty.Customer)
            Sort = Sort.Replace("Customer", "Customer.Name");
        Status = dto.Status;
    }
}