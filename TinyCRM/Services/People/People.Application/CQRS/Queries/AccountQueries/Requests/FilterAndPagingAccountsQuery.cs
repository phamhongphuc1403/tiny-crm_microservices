using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using People.Application.DTOs.AccountDTOs;

namespace People.Application.CQRS.Queries.AccountQueries.Requests;

public class FilterAndPagingAccountsQuery : FilterAndPagingAccountsDto,
    IQuery<FilterAndPagingResultDto<AccountSummaryDto>>
{
    public FilterAndPagingAccountsQuery(FilterAndPagingAccountsDto dto)
    {
        Keyword = dto.Keyword;
        Skip = dto.Skip;
        Take = dto.Take;
        IsDescending = dto.IsDescending;
        Sort = dto.ConvertSort();
    }

    public string Sort { get; private init; }
}