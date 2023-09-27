using BuildingBlock.Application.CQRS.Query;
using People.Application.DTOs.AccountDTOs;

namespace People.Application.CQRS.Queries.AccountQueries.Requests;

public class GetAllAccountsQuery : IQuery<IEnumerable<AccountSummaryDto>>
{
}