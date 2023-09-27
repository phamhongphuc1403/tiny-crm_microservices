using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Queries.AccountQueries.Requests;
using People.Application.DTOs.AccountDTOs;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Specifications;

namespace People.Application.CQRS.Queries.AccountQueries.Handlers;

public class FilterAccountsQueryHandler : IQueryHandler<FilterAccountsQuery, IEnumerable<AccountSummaryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Account> _readOnlyRepository;

    public FilterAccountsQueryHandler(IReadOnlyRepository<Account> readOnlyRepository, IMapper mapper)
    {
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AccountSummaryDto>> Handle(FilterAccountsQuery request,
        CancellationToken cancellationToken)
    {
        var accountNamePartialMatchSpecification = new AccountNamePartialMatchSpecification(request.Keyword);

        var accountEmailPartialMatchSpecification = new AccountEmailPartialMatchSpecification(request.Keyword);

        var specification = accountNamePartialMatchSpecification.Or(accountEmailPartialMatchSpecification);

        var accounts = await _readOnlyRepository.GetAllAsync(specification);

        return _mapper.Map<IEnumerable<AccountSummaryDto>>(accounts);
    }
}