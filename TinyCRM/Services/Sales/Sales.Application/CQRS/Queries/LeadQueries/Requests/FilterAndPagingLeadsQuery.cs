using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.Leads;

namespace Sales.Application.CQRS.Queries.LeadQueries.Requests;

public class FilterAndPagingLeadsQuery : FilterAndPagingLeadsDto, IQuery<FilterAndPagingResultDto<LeadSummaryDto>>
{
    public FilterAndPagingLeadsQuery(FilterAndPagingLeadsDto dto)
    {
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        Status = dto.Status;
    }

    public string Sort { get; private init; }
}