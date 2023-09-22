using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using People.Application.CQRS.Queries.AccountQueries.Requests;
using People.Application.DTOs.AccountDTOs;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Exceptions;
using People.Domain.AccountAggregate.Specifications;

namespace People.Application.CQRS.Queries.AccountQueries.Handlers;

public class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, AccountDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Account> _repository;

    public GetAccountByIdQueryHandler(
        IReadOnlyRepository<Account> repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AccountDetailDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var accountIdSpecification = new AccountIdSpecification(request.Id);

        var account = Optional<Account>.Of(await _repository.GetAnyAsync(accountIdSpecification))
            .ThrowIfNotPresent(new AccountNotFoundException(request.Id)).Get();

        return _mapper.Map<AccountDetailDto>(account);
    }
}