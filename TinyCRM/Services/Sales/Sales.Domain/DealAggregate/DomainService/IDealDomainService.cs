using Sales.Domain.DealAggregate.Enums;

namespace Sales.Domain.DealAggregate.DomainService;

public interface IDealDomainService
{
    Task<Deal> CreateDealAsync(string title, Guid customerId, Guid? leadId, string? description, DealStatus dealStatus,
        decimal estimatedRevenue);

    Task<Deal> UpdateDealAsync(Deal deal, string title, Guid customerId, Guid? leadId, string? description,
        DealStatus dealStatus,
        decimal estimatedRevenue);

    Task<Deal> DeleteManyDealAsync(List<Guid> ids);

    Task<Deal> CreateDealLineAsync(Deal deal, Guid productId, decimal price, int quantity);

    Task<Deal> UpdateDealLineAsync(Deal deal, Guid idDealLine, Guid productId, decimal price, int quantity);

    Task<Deal> DeleteManyDealLinesAsync(Deal deal, List<Guid> idDealLines);

    Task<Deal> UpdateDealStatusAsync(Deal deal, DealStatus dealStatus);
}