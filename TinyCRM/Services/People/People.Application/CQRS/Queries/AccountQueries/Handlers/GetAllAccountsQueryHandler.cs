using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Queries.AccountQueries.Requests;
using People.Application.DTOs.AccountDTOs;
using People.Domain.AccountAggregate.Entities;

namespace People.Application.CQRS.Queries.AccountQueries.Handlers;

public class GetAllAccountsQueryHandler : IQueryHandler<GetAllAccountsQuery, IEnumerable<AccountSummaryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Account> _readOnlyRepository;

    public GetAllAccountsQueryHandler(IReadOnlyRepository<Account> readOnlyRepository, IMapper mapper)
    {
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AccountSummaryDto>> Handle(GetAllAccountsQuery request,
        CancellationToken cancellationToken)
    {
        var accounts = await _readOnlyRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<AccountSummaryDto>>(accounts);
    }
}