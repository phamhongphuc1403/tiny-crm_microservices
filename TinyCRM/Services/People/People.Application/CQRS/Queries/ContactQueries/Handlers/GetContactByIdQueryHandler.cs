using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using People.Application.CQRS.Queries.ContactQueries.Requests;
using People.Application.DTOs.ContactDTOs;
using People.Domain.ContactAggregate.Entities;
using People.Domain.ContactAggregate.Exceptions;
using People.Domain.ContactAggregate.Specifications;

namespace People.Application.CQRS.Queries.ContactQueries.Handlers;

public record GetContactByIdQueryHandler : IQueryHandler<GetContactByIdQuery, ContactDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Contact> _repository;

    public GetContactByIdQueryHandler(IMapper mapper, IReadOnlyRepository<Contact> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ContactDetailDto> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contactIdSpecification = new ContactIdSpecification(request.Id);

        var contact = Optional<Contact>.Of(await _repository.GetAnyAsync(contactIdSpecification, "Account"))
            .ThrowIfNotPresent(new ContactNotFoundException(request.Id)).Get();

        return _mapper.Map<ContactDetailDto>(contact);
    }
}