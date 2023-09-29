using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Queries.ContactQueries.Requests;
using People.Application.DTOs.ContactDTOs;
using People.Domain.ContactAggregate.Entities;
using People.Domain.ContactAggregate.Specifications;

namespace People.Application.CQRS.Queries.ContactQueries.Handlers;

public class
    FilterAndPagingContactQueryHandler : IQueryHandler<FilterAndPagingContactQuery,
        FilterAndPagingResultDto<ContactSummaryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Contact> _repository;

    public FilterAndPagingContactQueryHandler(IMapper mapper, IReadOnlyRepository<Contact> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<FilterAndPagingResultDto<ContactSummaryDto>> Handle(FilterAndPagingContactQuery request,
        CancellationToken cancellationToken)
    {
        var contactNamePartialMatchSpecification = new ContactNamePartialMatchSpecification(request.Keyword);

        var contactEmailPartialMatchSpecification = new ContactEmailPartialMatchSpecification(request.Keyword);

        var specification = contactNamePartialMatchSpecification.Or(contactEmailPartialMatchSpecification);

        var (contacts, totalCount) =
            await _repository.GetFilterAndPagingAsync(specification, request.Sort, request.PageIndex, request.PageSize);

        return new FilterAndPagingResultDto<ContactSummaryDto>(_mapper.Map<List<ContactSummaryDto>>(contacts),
            request.PageIndex, request.PageSize, totalCount);
    }
}