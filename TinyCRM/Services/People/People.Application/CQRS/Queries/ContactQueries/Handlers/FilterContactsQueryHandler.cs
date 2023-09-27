using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Queries.ContactQueries.Requests;
using People.Application.DTOs.ContactDTOs;
using People.Domain.ContactAggregate.Entities;
using People.Domain.ContactAggregate.Specifications;

namespace People.Application.CQRS.Queries.ContactQueries.Handlers;

public class FilterContactsQueryHandler : IQueryHandler<FilterContactsQuery, IEnumerable<ContactSummaryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Contact> _readOnlyRepository;

    public FilterContactsQueryHandler(IMapper mapper, IReadOnlyRepository<Contact> readOnlyRepository)
    {
        _mapper = mapper;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<IEnumerable<ContactSummaryDto>> Handle(FilterContactsQuery request,
        CancellationToken cancellationToken)
    {
        var contactNamePartialMatchSpecification = new ContactNamePartialMatchSpecification(request.Keyword);

        var contactEmailPartialMatchSpecification = new ContactEmailPartialMatchSpecification(request.Keyword);

        var specification = contactNamePartialMatchSpecification.Or(contactEmailPartialMatchSpecification);

        var contacts = await _readOnlyRepository.GetAllAsync(specification);

        return _mapper.Map<IEnumerable<ContactSummaryDto>>(contacts);
    }
}