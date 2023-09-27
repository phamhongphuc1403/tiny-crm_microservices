using BuildingBlock.Application.CQRS.Query;
using People.Application.DTOs.ContactDTOs;

namespace People.Application.CQRS.Queries.ContactQueries.Requests;

public class FilterContactsQuery : FilterContactsDto, IQuery<IEnumerable<ContactSummaryDto>>
{
    public FilterContactsQuery(FilterContactsDto dto)
    {
        Keyword = dto.Keyword;
    }
}