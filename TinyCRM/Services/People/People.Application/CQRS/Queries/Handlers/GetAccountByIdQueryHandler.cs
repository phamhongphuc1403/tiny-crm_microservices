using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.Utils;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Queries.Requests;
using People.Application.DTOs;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Specifications;

namespace People.Application.CQRS.Queries.Handlers;

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