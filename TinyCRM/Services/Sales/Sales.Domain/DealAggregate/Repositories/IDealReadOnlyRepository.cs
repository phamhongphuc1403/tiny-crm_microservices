using BuildingBlock.Domain.Repositories;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Domain.DealAggregate.Repositories;

public interface IDealReadOnlyRepository : IReadOnlyRepository<Deal>
{
    public Task<(int OpenDeals, int DealsWon, double AvgRevenue, double TotalRevenue)>
        GetStatisticsAsync();

    public Task<List<(string Month, int Count)>> GetDealWonPerMonthAsync();
    public Task<List<(string Month, double TotalRevenue)>> GetTotalRevenuePerMonthAsync();
}