using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Queries.AccountQueries.Requests;
using Sales.Application.DTOs.Accounts;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.AccountQueries.Handlers;

public class FilterAccountsQueryHandler:IQueryHandler<FilterAccountsQuery,FilterResultDto<AccountResultDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Account> _repository;

    public FilterAccountsQueryHandler(IMapper mapper, IReadOnlyRepository<Account> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<FilterResultDto<AccountResultDto>> Handle(FilterAccountsQuery request, CancellationToken cancellationToken)
    {
        var accountNameSpecification = new AccountNamePartialMatchSpecification(request.Keyword);
        var accounts = await _repository.GetAllAsync(accountNameSpecification);
        return new FilterResultDto<AccountResultDto>(_mapper.Map<List<AccountResultDto>>(accounts));
    }
}