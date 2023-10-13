using BuildingBlock.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Enums;
using Sales.Domain.DealAggregate.Repositories;

namespace Sales.Infrastructure.EFCore.Repositories.DealRepositories;

public class DealReadOnlyRepository : ReadOnlyRepository<SaleDbContext, Deal>, IDealReadOnlyRepository
{
    public DealReadOnlyRepository(SaleDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(int OpenDeals, int DealsWon, double AvgRevenue, double TotalRevenue)> GetStatisticsAsync()
    {
        var query = DbSet.AsQueryable();
        if (!await query.AnyAsync())
            return (0, 0, 0, 0);
        var openDeals = await query.CountAsync(x => x.DealStatus == DealStatus.Open);
        var dealsWon = await query.CountAsync(x => x.DealStatus == DealStatus.Won);
        
        if (dealsWon == 0)
            return (openDeals, dealsWon, 0, 0);

        var avgRevenue = await query.Where(x => x.DealStatus == DealStatus.Won)
            .AverageAsync(x => x.ActualRevenue);
        var totalRevenue = await query.Where(x => x.DealStatus == DealStatus.Won)
            .SumAsync(x => x.ActualRevenue);
        return (openDeals, dealsWon, avgRevenue, totalRevenue);
    }
}