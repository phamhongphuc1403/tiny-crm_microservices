using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
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
            Cards = new List<CardDto>
            {
                new()
                {
                    Name = DealNameStatisticsDto.OpenDeals,
                    Value = openDeals,
                    IsPrice = false
                },
                new()
                {
                    Name = DealNameStatisticsDto.DealsWon,
                    Value = wonDeals,
                    IsPrice = false
                },
                new()
                {
                    Name = DealNameStatisticsDto.AverageRevenue,
                    Value = avgRevenue,
                    IsPrice = true
                },
                new()
                {
                    Name = DealNameStatisticsDto.TotalRevenue,
                    Value = totalRevenue,
                    IsPrice = true
                }
            }
        };
    }
}