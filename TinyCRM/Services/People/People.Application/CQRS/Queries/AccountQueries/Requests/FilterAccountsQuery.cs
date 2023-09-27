using BuildingBlock.Application.CQRS.Query;
using People.Application.DTOs.AccountDTOs;

namespace People.Application.CQRS.Queries.AccountQueries.Requests;

public class FilterAccountsQuery : FilterAccountsDto, IQuery<IEnumerable<AccountSummaryDto>>
{
    public FilterAccountsQuery(FilterAccountsDto dto)
    {
        Keyword = dto.Keyword;
    }
}