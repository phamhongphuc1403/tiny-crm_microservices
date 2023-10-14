using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using People.Application.CQRS.Queries.ContactQueries.Requests;
using People.Application.DTOs.ContactDTOs;
using People.Domain.AccountAggregate.Entities;
using People.Domain.AccountAggregate.Exceptions;
using People.Domain.AccountAggregate.Specifications;
using People.Domain.ContactAggregate.Entities;
using People.Domain.ContactAggregate.Specifications;

namespace People.Application.CQRS.Queries.ContactQueries.Handlers;

public class FilterAndPagingContactsByAccountIdQueryHandler : IQueryHandler<FilterAndPagingContactsByAccountIdQuery,
    FilterAndPagingResultDto<ContactSummaryDto>>
{
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;
    private readonly IReadOnlyRepository<Contact> _contactReadOnlyRepository;
    private readonly IMapper _mapper;

    public FilterAndPagingContactsByAccountIdQueryHandler(IReadOnlyRepository<Contact> contactReadOnlyRepository,
        IMapper mapper, IReadOnlyRepository<Account> accountReadOnlyRepository)
    {
        _contactReadOnlyRepository = contactReadOnlyRepository;
        _mapper = mapper;
        _accountReadOnlyRepository = accountReadOnlyRepository;
    }

    public async Task<FilterAndPagingResultDto<ContactSummaryDto>> Handle(
        FilterAndPagingContactsByAccountIdQuery request,
        CancellationToken cancellationToken)
    {
        var accountIdSpecification = new AccountIdSpecification(request.AccountId);

        Optional<bool>.Of(await _accountReadOnlyRepository.CheckIfExistAsync(accountIdSpecification))
            .ThrowIfNotPresent(new AccountNotFoundException(request.AccountId));

        var contactAccountIdSpecification = new ContactAccountIdSpecification(request.AccountId);

        var contactNamePartialMatchSpecification = new ContactNamePartialMatchSpecification(request.Keyword);

        var contactEmailPartialMatchSpecification = new ContactEmailPartialMatchSpecification(request.Keyword);

        var specification =
            contactAccountIdSpecification.And(
                contactNamePartialMatchSpecification.Or(contactEmailPartialMatchSpecification));

        var (contacts, totalCount) =
            await _contactReadOnlyRepository.GetFilterAndPagingAsync(specification, request.Sort, request.Skip,
                request.Take);

        return new FilterAndPagingResultDto<ContactSummaryDto>(_mapper.Map<List<ContactSummaryDto>>(contacts),
            totalCount);
    }
}