using BuildingBlock.Domain.Repositories;

namespace Sales.Domain.LeadAggregate.Repositories;

public interface ILeadReadOnlyRepository : IReadOnlyRepository<Lead>
{
    public Task<(int OpenLeads, int QualifiedLeads, int DisqualifiedLeads, double AvgEstimatedRevenue)>
        GetStatisticsAsync();
}