using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using People.Application.DTOs.ContactDTOs;

namespace People.Application.CQRS.Queries.ContactQueries.Requests;

public class FilterAndPagingContactQuery : FilterAndPagingContactsDto, IQuery<FilterAndPagingResultDto<ContactSummaryDto>>
{
    public FilterAndPagingContactQuery(FilterAndPagingContactsDto dto)
    {
        Keyword = dto.Keyword;
        PageIndex = dto.PageIndex;
        PageSize = dto.PageSize;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
    }
    public string Sort { get; private init; }
}