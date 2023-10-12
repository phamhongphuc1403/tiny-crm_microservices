using BuildingBlock.Application.CQRS.Query;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Queries.DealQueries.Requests;

public class GetDeaLineByIdQuery : IQuery<DealLineDto>
{
    public GetDeaLineByIdQuery(Guid dealId, Guid dealLineId)
    {
        DealId = dealId;
        DealLineId = dealLineId;
    }

    public Guid DealId { get; private set; }
    public Guid DealLineId { get; private set; }
}