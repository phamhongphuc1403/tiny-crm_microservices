using BuildingBlock.Application.CQRS.Query;
using Sales.Application.DTOs;

namespace Sales.Application.CQRS.Queries.Requests;

public class GetLeadQuery : IQuery<LeadDto>
{
    public GetLeadQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}