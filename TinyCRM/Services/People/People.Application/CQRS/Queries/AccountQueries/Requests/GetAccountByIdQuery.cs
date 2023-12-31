using BuildingBlock.Application.CQRS.Query;
using People.Application.DTOs.AccountDTOs;

namespace People.Application.CQRS.Queries.AccountQueries.Requests;

public class GetAccountByIdQuery : IQuery<AccountDetailDto>
{
    public GetAccountByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private init; }
}