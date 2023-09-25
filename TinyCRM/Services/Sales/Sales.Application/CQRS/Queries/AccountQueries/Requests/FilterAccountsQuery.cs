using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.DTOs.Accounts;

namespace Sales.Application.CQRS.Queries.AccountQueries.Requests;

public class FilterAccountsQuery : FilterAccountDto,IQuery<FilterResultDto<AccountResultDto>>
{
    public FilterAccountsQuery(FilterAccountDto dto)
    {
        Keyword = dto.Keyword;
    }
}