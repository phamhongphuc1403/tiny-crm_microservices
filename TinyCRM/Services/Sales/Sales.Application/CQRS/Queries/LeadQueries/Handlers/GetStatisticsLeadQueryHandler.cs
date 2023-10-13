using BuildingBlock.Application.CQRS.Query;
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
        return new LeadStatisticsDto()
        {
            OpenLeads = openLeads,
            QualifiedLeads = qualifiedLeads,
            DisqualifiedLeads = disqualifiedLeads,
            AvgEstimatedRevenue = avgEstimatedRevenue
        };
    }
}