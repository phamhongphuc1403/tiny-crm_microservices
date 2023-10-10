using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Queries.AccountQueries.Requests;
using Sales.Application.DTOs.AccountDTOs;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.AccountQueries.Handlers;

public class FilterAndPagingAccountsHandler : IQueryHandler<FilterAndPagingAccountsQuery, FilterAndPagingResultDto<AccountSummaryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Account> _repository;

    public FilterAndPagingAccountsHandler(IReadOnlyRepository<Account> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<AccountSummaryDto>> Handle(FilterAndPagingAccountsQuery request,
        CancellationToken cancellationToken)
    {
        var accountNameSpecification = new AccountNamePartialMatchSpecification(request.Keyword);
        var (accounts,totalCount) = await _repository.GetFilterAndPagingAsync(accountNameSpecification,
            request.Sort, request.Skip, request.Take);
        return new FilterAndPagingResultDto<AccountSummaryDto>(_mapper.Map<List<AccountSummaryDto>>(accounts), request.Skip,
            request.Take,
            totalCount);
    }
}