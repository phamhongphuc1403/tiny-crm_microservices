using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Queries.AccountQueries.Requests;
using Sales.Application.DTOs.Accounts;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.AccountQueries.Handlers;

public class FilterAccountsQueryHandler : IQueryHandler<FilterAccountsQuery, List<AccountResultDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Account> _repository;

    public FilterAccountsQueryHandler(IMapper mapper, IReadOnlyRepository<Account> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<AccountResultDto>> Handle(FilterAccountsQuery request, CancellationToken cancellationToken)
    {
        var accountNameSpecification = new AccountNamePartialMatchSpecification(request.Keyword);
        var accounts = await _repository.GetAllAsync(accountNameSpecification);
        return _mapper.Map<List<AccountResultDto>>(accounts);
    }
}