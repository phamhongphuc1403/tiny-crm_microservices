using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using People.Application.DTOs.ContactDTOs;

namespace People.Application.CQRS.Queries.ContactQueries.Requests;

public class FilterAndPagingContactsByAccountIdQuery : FilterAndPagingContactsDto,
    IQuery<FilterAndPagingResultDto<ContactSummaryDto>>
{
    public FilterAndPagingContactsByAccountIdQuery(Guid accountId, FilterAndPagingContactsDto dto)
    {
        AccountId = accountId;
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        Sort = dto.ConvertSort();
    }

    public Guid AccountId { get; }
    public string Sort { get; }
}