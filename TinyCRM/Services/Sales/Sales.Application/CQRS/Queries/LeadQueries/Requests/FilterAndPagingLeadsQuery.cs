using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.Leads;

namespace Sales.Application.CQRS.Queries.LeadQueries.Requests;

public class FilterAndPagingLeadsQuery : FilterAndPagingLeadsDto, IQuery<FilterAndPagingResultDto<LeadDto>>
{
    public FilterAndPagingLeadsQuery(FilterAndPagingLeadsDto dto)
    {
        Keyword = dto.Keyword;
        PageIndex = dto.PageIndex;
        PageSize = dto.PageSize;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
        Status = dto.Status;
    }

    public string Sort { get; private init; }
}