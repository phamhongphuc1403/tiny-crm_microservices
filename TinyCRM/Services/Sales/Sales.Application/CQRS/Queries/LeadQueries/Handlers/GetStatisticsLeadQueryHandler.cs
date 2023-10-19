using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.CQRS.Queries.LeadQueries.Requests;
using Sales.Application.DTOs.LeadDTOs;
using Sales.Domain.LeadAggregate.Repositories;

namespace Sales.Application.CQRS.Queries.LeadQueries.Handlers;

public class GetStatisticsLeadQueryHandler : IQueryHandler<GetStatisticsLeadQuery, LeadStatisticsDto>
{
    private readonly ILeadReadOnlyRepository _leadReadOnlyRepository;

    public GetStatisticsLeadQueryHandler(ILeadReadOnlyRepository leadReadOnlyRepository)
    {
        _leadReadOnlyRepository = leadReadOnlyRepository;
    }

    public async Task<LeadStatisticsDto> Handle(GetStatisticsLeadQuery request, CancellationToken cancellationToken)
    {
        var (openLeads, qualifiedLeads, disqualifiedLeads, avgEstimatedRevenue) =
            await _leadReadOnlyRepository.GetStatisticsAsync();
        var statistics = new LeadStatisticsDto
        {
            Cards = new List<CardDto>
            {
                new()
                {
                    Name = LeadNameStatisticsDto.OpenLeads,
                    Value = openLeads,
                    IsPrice = false
                },
                new()
                {
                    Name = LeadNameStatisticsDto.QualifiedLeads,
                    Value = qualifiedLeads,
                    IsPrice = false
                },
                new()
                {
                    Name = LeadNameStatisticsDto.DisqualifiedLeads,
                    Value = disqualifiedLeads,
                    IsPrice = false
                },
                new()
                {
                    Name = LeadNameStatisticsDto.AverageEstimatedRevenue,
                    Value = avgEstimatedRevenue,
                    IsPrice = true
                }
            }
        };
        return statistics;
    }
}