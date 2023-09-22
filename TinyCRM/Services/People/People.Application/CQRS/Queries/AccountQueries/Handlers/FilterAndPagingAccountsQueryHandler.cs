using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Queries.AccountQueries.Requests;
using People.Application.DTOs.AccountDTOs;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Specifications;

namespace People.Application.CQRS.Queries.AccountQueries.Handlers;

public class
    FilterAndPagingAccountsQueryHandler : IQueryHandler<FilterAndPagingAccountsQuery,
        FilterAndPagingResultDto<AccountSummaryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Account> _repository;

    public FilterAndPagingAccountsQueryHandler
    (
        IReadOnlyRepository<Account> repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<AccountSummaryDto>> Handle(FilterAndPagingAccountsQuery request,
        CancellationToken cancellationToken)
    {
        var accountNamePartialMatchSpecification = new AccountNamePartialMatchSpecification(request.Keyword);

        var accountEmailPartialMatchSpecification = new AccountEmailPartialMatchSpecification(request.Keyword);

        var specification = accountNamePartialMatchSpecification.Or(accountEmailPartialMatchSpecification);

        var (accounts, totalCount) =
            await _repository.GetFilterAndPagingAsync(specification, request.Sort, request.PageIndex, request.PageSize);

        return new FilterAndPagingResultDto<AccountSummaryDto>(_mapper.Map<List<AccountSummaryDto>>(accounts),
            request.PageIndex, request.PageSize, totalCount);
    }
}