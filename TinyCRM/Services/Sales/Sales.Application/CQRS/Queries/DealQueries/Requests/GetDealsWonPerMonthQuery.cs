using BuildingBlock.Application.CQRS.Query;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Queries.DealQueries.Requests;

public class GetDealsWonPerMonthQuery : IQuery<DealStatisticsDto>
{
}