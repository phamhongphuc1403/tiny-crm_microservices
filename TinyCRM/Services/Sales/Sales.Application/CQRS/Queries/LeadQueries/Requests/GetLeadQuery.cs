using BuildingBlock.Application.CQRS.Query;
using Sales.Application.DTOs.Leads;

namespace Sales.Application.CQRS.Queries.LeadQueries.Requests;

public class GetLeadQuery : IQuery<LeadDto>
{
    public GetLeadQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}