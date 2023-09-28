using BuildingBlock.Application.CQRS.Query;
using Sales.Application.DTOs.Accounts;

namespace Sales.Application.CQRS.Queries.AccountQueries.Requests;

public class FilterAccountsQuery : FilterAccountDto, IQuery<List<AccountResultDto>>
{
    public FilterAccountsQuery(FilterAccountDto dto)
    {
        Keyword = dto.Keyword;
    }
}