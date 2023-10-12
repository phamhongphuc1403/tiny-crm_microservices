using BuildingBlock.Application.CQRS.Query;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Queries.DealQueries.Requests;

public class GetDealByLeadIdQuery:IQuery<DealDetailDto>
{
    public Guid LeadId { get; private set; }
    public GetDealByLeadIdQuery(Guid leadId)
    {
        LeadId = leadId;
    }
}