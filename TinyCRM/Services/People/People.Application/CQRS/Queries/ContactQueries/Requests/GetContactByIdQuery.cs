using BuildingBlock.Application.CQRS.Query;
using People.Application.DTOs.ContactDTOs;

namespace People.Application.CQRS.Queries.ContactQueries.Requests;

public record GetContactByIdQuery : IQuery<ContactDetailDto>
{
    public GetContactByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private init; }
}