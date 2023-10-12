using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Enums;

namespace Sales.Domain.DealAggregate.DomainService;

public interface IDealDomainService
{
    Task<Deal> CreateDealAsync(string title, Guid customerId, Guid? leadId, string? description,
        double estimatedRevenue, double actualRevenue);

    Task<Deal> CreateDealAsync(Guid dealId, string title, Guid customerId, Guid? leadId, string? description,
        double estimatedRevenue, double actualRevenue);

    Task<Deal> UpdateDealAsync(Deal deal, string title, Guid customerId, Guid? leadId, string? description,
        double estimatedRevenue, double actualRevenue);

    Task<IList<Deal>> DeleteManyDealAsync(IEnumerable<Guid> ids);

    Task<Deal> GetDealAsync(Guid id);

    Deal UpdateStatus(Deal deal, DealStatus dealStatus);

    Task<DealLine> CreateDealLineAsync(Deal deal, Guid productId, double price, int quantity);

    Task<Deal> UpdateDealLineAsync(Deal deal, Guid idDealLine, Guid productId, decimal price, int quantity);

    void RemoveDealLines(Deal deal, IEnumerable<Guid> dealLineIds);

    void RemoveDealLines(Deal deal, IEnumerable<DealLine> dealLines);

    Task<Deal> UpdateDealStatusAsync(Deal deal, DealStatus dealStatus);

    DealLine GetDealLine(Deal deal, Guid dealLineId);

    Task<DealLine> EditDealLineAsync(Deal deal, Guid dealLineId, Guid productId, int quantity, double pricePerUnit);
}