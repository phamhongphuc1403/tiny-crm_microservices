using BuildingBlock.Application.CQRS.Query;
using Sales.Application.DTOs.LeadDTOs;

namespace Sales.Application.CQRS.Queries.LeadQueries.Requests;

public class GetLeadQuery : IQuery<LeadDetailDto>
{
    public GetLeadQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}