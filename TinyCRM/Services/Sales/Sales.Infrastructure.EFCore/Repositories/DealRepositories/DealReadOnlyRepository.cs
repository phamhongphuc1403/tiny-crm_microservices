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

    public async Task<List<(string Month, int Count)>> GetDealWonPerMonthAsync()
    {
        var currentDate = DateTime.Now;
        var oneYearAgo = currentDate.AddYears(-1);
        var monthsList = new List<DateTime>();
        while (oneYearAgo <= currentDate)
        {
            monthsList.Add(oneYearAgo);
            oneYearAgo = oneYearAgo.AddMonths(1);
        }

        var tuples = new List<(string Month, int Count)>();
        foreach (var month in monthsList)
        {
            var count = await DbSet.CountAsync(deal => deal.DealStatus == DealStatus.Won
                                                       && deal.CloseDateTime != null
                                                       && deal.CloseDateTime.Value.Year == month.Year
                                                       && deal.CloseDateTime.Value.Month == month.Month);
            tuples.Add((Month: month.ToString("MM/yyyy"), Count: count));
        }

        return tuples;
    }


    public async Task<List<(string Month, double TotalRevenue)>> GetTotalRevenuePerMonthAsync()
    {
        var currentDate = DateTime.Now;
        var oneYearAgo = currentDate.AddYears(-1);
        var monthsList = new List<DateTime>();
        while (oneYearAgo <= currentDate)
        {
            monthsList.Add(oneYearAgo);
            oneYearAgo = oneYearAgo.AddMonths(1);
        }

        var tuples = new List<(string Month, double TotalRevenue)>();
        foreach (var month in monthsList)
        {
            var totalRevenue = await DbSet.Where(deal => deal.DealStatus == DealStatus.Won
                                                         && deal.CloseDateTime != null
                                                         && deal.CloseDateTime.Value.Year == month.Year
                                                         && deal.CloseDateTime.Value.Month == month.Month)
                .SumAsync(deal => deal.ActualRevenue);
            tuples.Add((Month: month.ToString("MM/yyyy"), TotalRevenue: totalRevenue));
        }

        return tuples;
    }
}