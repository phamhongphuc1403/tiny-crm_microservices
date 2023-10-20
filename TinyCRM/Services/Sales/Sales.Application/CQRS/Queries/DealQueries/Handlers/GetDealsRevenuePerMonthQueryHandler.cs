using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.CQRS.Queries.DealQueries.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.Repositories;

namespace Sales.Application.CQRS.Queries.DealQueries.Handlers;

public class GetDealsRevenuePerMonthQueryHandler : IQueryHandler<GetDealsRevenuePerMonthQuery, DealStatisticsDto>
{
    private readonly IDealReadOnlyRepository _dealReadOnlyRepository;

    public GetDealsRevenuePerMonthQueryHandler(IDealReadOnlyRepository dealReadOnlyRepository)
    {
        _dealReadOnlyRepository = dealReadOnlyRepository;
    }

    public async Task<DealStatisticsDto> Handle(GetDealsRevenuePerMonthQuery request, CancellationToken cancellationToken)
    {
        var dealsWonPerMonth = await _dealReadOnlyRepository.GetTotalRevenuePerMonthAsync();
        var cardDtos = dealsWonPerMonth
            .Select(monthlyData => new CardDto
            {
                Name = monthlyData.Month,
                Value = monthlyData.TotalRevenue,
                IsPrice = true
            })
            .ToList();

        var dealsWonPerMonthDto = new DealStatisticsDto
        {
            Cards = cardDtos
        };

        return dealsWonPerMonthDto;
    }
}