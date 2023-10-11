using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.Exceptions;
using Sales.Domain.AccountAggregate.Specifications;
using Sales.Domain.DealAggregate.Enums;
using Sales.Domain.DealAggregate.Events;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Enums;
using Sales.Domain.LeadAggregate.Exceptions;
using Sales.Domain.LeadAggregate.Specifications;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.DealAggregate.DomainService;

public class DealDomainService : IDealDomainService
{
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;
    private readonly IReadOnlyRepository<Deal> _dealReadOnlyRepository;
    private readonly IReadOnlyRepository<Product> _productReadOnlyRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadOnlyRepository;

    public DealDomainService(IReadOnlyRepository<Deal> dealReadOnlyRepository,
        IReadOnlyRepository<Product> productReadOnlyRepository, IReadOnlyRepository<Account> accountReadOnlyRepository,
        IReadOnlyRepository<Lead> leadReadOnlyRepository)
    {
        _dealReadOnlyRepository = dealReadOnlyRepository;
        _productReadOnlyRepository = productReadOnlyRepository;
        _accountReadOnlyRepository = accountReadOnlyRepository;
        _leadReadOnlyRepository = leadReadOnlyRepository;
    }

    public async Task<Deal> CreateDealAsync(Guid dealId, string title, Guid customerId, Guid? leadId,
        string? description,
        double estimatedRevenue, double actualRevenue)
    {
        Optional<bool>
            .Of(await _accountReadOnlyRepository.CheckIfExistAsync(new AccountIdSpecification(customerId)))
            .ThrowIfNotPresent(new AccountNotFoundException(customerId));

        if (leadId == null)
            return new Deal(dealId, title, customerId, leadId, description, estimatedRevenue, actualRevenue);

        var leads = Optional<List<Lead>>
            .Of(await _leadReadOnlyRepository.GetAllAsync(new LeadAccountIdMatchSpecification(customerId)))
            .ThrowIfNotPresent(new LeadNotFoundException(leadId.Value))
            .Get().ToList();
        var lead = leads.FirstOrDefault(l => l.Id == (Guid)leadId);
        if (lead == null)
            throw new LeadMatchAccountException($"Lead {leadId} not match account {customerId}");
        return new Deal(dealId, title, customerId, leadId, description, estimatedRevenue, actualRevenue);
    }

    private static void CheckValidStatus(LeadStatus status)
    {
        if (status is LeadStatus.Qualified or LeadStatus.Disqualified)
            throw new LeadValidStatusException(status);
    }

    public async Task<Deal> CreateDealAsync(string title, Guid customerId, Guid? leadId, string? description,
        double estimatedRevenue, double actualRevenue)
    {
        Optional<bool>
            .Of(await _accountReadOnlyRepository.CheckIfExistAsync(new AccountIdSpecification(customerId)))
            .ThrowIfNotPresent(new AccountNotFoundException(customerId));

        if (leadId == null) return new Deal(title, customerId, leadId, description, estimatedRevenue, actualRevenue);

        var leads = Optional<List<Lead>>
            .Of(await _leadReadOnlyRepository.GetAllAsync(new LeadAccountIdMatchSpecification(customerId)))
            .ThrowIfNotPresent(new LeadNotFoundException(leadId.Value))
            .Get().ToList();
        var lead = leads.FirstOrDefault(l => l.Id == (Guid)leadId);
        if (lead == null)
            throw new LeadMatchAccountException($"Lead {leadId} not match account {customerId}");
        CheckValidStatus(lead.Status);
        var deal = new Deal(title, customerId, leadId, description, estimatedRevenue, actualRevenue);
        deal.AddDomainEvent(new CreatedDealEvent((Guid)leadId));
        return deal;
    }

    public Task<Deal> UpdateDealAsync(Deal deal, string title, Guid customerId, Guid? leadId, string? description,
        DealStatus dealStatus,
        double estimatedRevenue, double actualRevenue)
    {
        throw new NotImplementedException();
    }

    public Task<Deal> DeleteManyDealAsync(List<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public Task<Deal> CreateDealLineAsync(Deal deal, Guid productId, decimal price, int quantity)
    {
        throw new NotImplementedException();
    }

    public Task<Deal> UpdateDealLineAsync(Deal deal, Guid idDealLine, Guid productId, decimal price, int quantity)
    {
        throw new NotImplementedException();
    }

    public Task<Deal> DeleteManyDealLinesAsync(Deal deal, List<Guid> idDealLines)
    {
        throw new NotImplementedException();
    }

    public Task<Deal> UpdateDealStatusAsync(Deal deal, DealStatus dealStatus)
    {
        throw new NotImplementedException();
    }
}