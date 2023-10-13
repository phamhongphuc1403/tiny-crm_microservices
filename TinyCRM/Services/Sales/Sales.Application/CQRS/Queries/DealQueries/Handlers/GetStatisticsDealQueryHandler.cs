using BuildingBlock.Application.CQRS.Query;
using Sales.Application.CQRS.Queries.DealQueries.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.Repositories;

namespace Sales.Application.CQRS.Queries.DealQueries.Handlers;

public class GetStatisticsDealQueryHandler : IQueryHandler<GetStatisticsDealQuery, DealStatisticsDto>
{
    private readonly IDealReadOnlyRepository _dealReadOnlyRepository;

    public GetStatisticsDealQueryHandler(IDealReadOnlyRepository dealReadOnlyRepository)
    {
        _dealReadOnlyRepository = dealReadOnlyRepository;
    }

    public async Task<DealStatisticsDto> Handle(GetStatisticsDealQuery request, CancellationToken cancellationToken)
    {
        var (openDeals, wonDeals, avgRevenue, totalRevenue) =
            await _dealReadOnlyRepository.GetStatisticsAsync();
        return new DealStatisticsDto
        {
            OpenDeals = openDeals,
            DealsWon = wonDeals,
            AvgRevenue = avgRevenue,
            TotalRevenue = totalRevenue
        };
    }
}