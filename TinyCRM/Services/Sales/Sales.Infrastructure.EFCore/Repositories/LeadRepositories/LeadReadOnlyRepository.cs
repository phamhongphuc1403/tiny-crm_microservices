using BuildingBlock.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Enums;
using Sales.Domain.LeadAggregate.Repositories;

namespace Sales.Infrastructure.EFCore.Repositories.LeadRepositories;

public class LeadReadOnlyRepository : ReadOnlyRepository<SaleDbContext, Lead>, ILeadReadOnlyRepository
{
    public LeadReadOnlyRepository(SaleDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<(int OpenLeads, int QualifiedLeads, int DisqualifiedLeads, double AvgEstimatedRevenue)>
        GetStatisticsAsync()
    {
        var query = DbSet.AsQueryable();
        if (!await query.AnyAsync())
            return (0, 0, 0, 0);
        var openLeads = await query.CountAsync(x => x.Status == LeadStatus.Open);
        var qualifiedLeads = await query.CountAsync(x => x.Status == LeadStatus.Qualified);
        var disqualifiedLeads = await query.CountAsync(x => x.Status == LeadStatus.Disqualified);
        var avgEstimatedRevenue = await query.AverageAsync(x => x.EstimatedRevenue);
        return (openLeads, qualifiedLeads, disqualifiedLeads, avgEstimatedRevenue);
    }
}