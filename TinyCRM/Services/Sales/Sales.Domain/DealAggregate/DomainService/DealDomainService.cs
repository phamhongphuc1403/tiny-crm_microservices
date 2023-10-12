using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.Exceptions;
using Sales.Domain.AccountAggregate.Specifications;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Enums;
using Sales.Domain.DealAggregate.Events;
using Sales.Domain.DealAggregate.Exceptions;
using Sales.Domain.DealAggregate.Specifications;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Enums;
using Sales.Domain.LeadAggregate.Exceptions;
using Sales.Domain.LeadAggregate.Specifications;
using Sales.Domain.ProductAggregate.Entities;
using Sales.Domain.ProductAggregate.Exceptions;
using Sales.Domain.ProductAggregate.Specifications;

namespace Sales.Domain.DealAggregate.DomainService;

public class DealDomainService : IDealDomainService
{
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;
    private readonly IReadOnlyRepository<Deal> _dealReadOnlyRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadOnlyRepository;
    private readonly IReadOnlyRepository<Product> _productReadOnlyRepository;

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

    public async Task<Deal> UpdateDealAsync(Deal deal, string title, Guid customerId, Guid? leadId, string? description,
        double estimatedRevenue, double actualRevenue)
    {
        if (deal.DealStatus is DealStatus.Won or DealStatus.Lost)
        {
            deal.Description = description;
            return deal;
        }

        Optional<bool>
            .Of(await _accountReadOnlyRepository.CheckIfExistAsync(new AccountIdSpecification(customerId)))
            .ThrowIfNotPresent(new AccountNotFoundException(customerId));

        if (leadId == null)
        {
            deal.Update(title, customerId, leadId, description, estimatedRevenue, actualRevenue);
            return deal;
        }

        await ValidateUpdateDeal((Guid)leadId, customerId);

        if (deal.LeadId != leadId)
        {
            deal.AddDomainEvent(new ChangedLeadIdForDealEvent(deal.LeadId, (Guid)leadId));
        }

        deal.Update(title, customerId, leadId, description, estimatedRevenue, actualRevenue);
        return deal;
    }

    private async Task ValidateUpdateDeal(Guid leadId, Guid customerId)
    {
        var leadIdSpecification = new LeadIdSpecification(leadId);

        var lead = Optional<Lead>.Of(await _leadReadOnlyRepository.GetAnyAsync(leadIdSpecification))
            .ThrowIfNotPresent(new LeadNotFoundException(leadId)).Get();

        if (lead.Status is LeadStatus.Disqualified or LeadStatus.Qualified)
            throw new LeadValidStatusException(lead.Status);

        var leadAccountIdSpecification = new LeadAccountIdMatchSpecification(customerId);
        Optional<bool>.Of(
                await _leadReadOnlyRepository.CheckIfExistAsync(leadIdSpecification.And(leadAccountIdSpecification)))
            .ThrowIfNotPresent(new LeadMatchAccountException($"Lead {leadId} not match account {customerId}"));
    }

    public async Task<Deal> GetDealAsync(Guid id)
    {
        var dealIdSpecification = new DealIdSpecification(id);

        const string includeTables = "DealLines";

        return Optional<Deal>.Of(await _dealReadOnlyRepository.GetAnyAsync(dealIdSpecification, includeTables))
            .ThrowIfNotPresent(new DealNotfoundException(id)).Get();
    }

    public Deal UpdateStatus(Deal deal, DealStatus dealStatus)
    {
        if (deal.DealStatus != DealStatus.Open)
            throw new DealValidStatusException(deal.DealStatus);

        deal.DealStatus = dealStatus;
        return deal;
    }

    public Task<Deal> DeleteManyDealAsync(List<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public async Task<DealLine> CreateDealLineAsync(Deal deal, Guid productId, double price, int quantity)
    {
        var productIdSpecification = new ProductIdSpecification(productId);

        Optional<Product>.Of(await _productReadOnlyRepository.GetAnyAsync(productIdSpecification))
            .ThrowIfNotPresent(new ProductNotFoundException(productId));

        return deal.AddDealLine(productId, price, quantity);
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

    private static void CheckValidStatus(LeadStatus status)
    {
        if (status is LeadStatus.Qualified or LeadStatus.Disqualified)
            throw new LeadValidStatusException(status);
    }
}