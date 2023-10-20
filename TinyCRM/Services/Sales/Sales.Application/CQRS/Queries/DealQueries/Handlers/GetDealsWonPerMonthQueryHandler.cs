using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.CQRS.Queries.DealQueries.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.Repositories;

namespace Sales.Application.CQRS.Queries.DealQueries.Handlers;

public class GetDealsWonPerMonthQueryHandler : IQueryHandler<GetDealsWonPerMonthQuery, DealStatisticsDto>
{
    private readonly IDealReadOnlyRepository _dealReadOnlyRepository;

    public GetDealsWonPerMonthQueryHandler(IDealReadOnlyRepository dealReadOnlyRepository)
    {
        _dealReadOnlyRepository = dealReadOnlyRepository;
    }

    public async Task<DealStatisticsDto> Handle(GetDealsWonPerMonthQuery request, CancellationToken cancellationToken)
    {
        var dealsWonPerMonth = await _dealReadOnlyRepository.GetDealWonPerMonthAsync();
        var cardDtos = dealsWonPerMonth
            .Select(monthlyData => new CardDto
            {
                Name = monthlyData.Month,
                Value = monthlyData.Count,
                IsPrice = false
            })
            .ToList();

        var dealsWonPerMonthDto = new DealStatisticsDto
        {
            Cards = cardDtos
        };

        return dealsWonPerMonthDto;
    }
}