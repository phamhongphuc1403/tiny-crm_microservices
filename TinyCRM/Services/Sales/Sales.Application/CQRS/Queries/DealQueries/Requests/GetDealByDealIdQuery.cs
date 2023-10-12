using BuildingBlock.Application.CQRS.Query;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Queries.DealQueries.Requests;

public class GetDealByDealIdQuery:IQuery<DealDetailDto>
{
    public Guid DealId { get; private set; }
    public GetDealByDealIdQuery(Guid dealId)
    {
        DealId = dealId;
    }
}