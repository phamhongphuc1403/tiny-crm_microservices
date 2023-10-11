using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.Exceptions;
using Sales.Domain.AccountAggregate.Specifications;
using Sales.Domain.LeadAggregate.Enums;
using Sales.Domain.LeadAggregate.Events;
using Sales.Domain.LeadAggregate.Exceptions;
using Sales.Domain.LeadAggregate.Specifications;

namespace Sales.Domain.LeadAggregate.DomainService;

public class LeadDomainService : ILeadDomainService
{
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadOnlyRepository;

    public LeadDomainService(IReadOnlyRepository<Account> accountReadOnlyRepository,
        IReadOnlyRepository<Lead> leadReadOnlyRepository)
    {
        _accountReadOnlyRepository = accountReadOnlyRepository;
        _leadReadOnlyRepository = leadReadOnlyRepository;
    }

    public async Task<Lead> CreateAsync(string title, Guid customerId, LeadSource? source, double? estimatedRevenue,
        string? description)
    {
        Optional<bool>
            .Of(await _accountReadOnlyRepository.CheckIfExistAsync(new AccountIdSpecification(customerId)))
            .ThrowIfNotPresent(new AccountNotFoundException(customerId));

        return new Lead(title, customerId, source, estimatedRevenue, description);
    }

    public async Task<Lead> UpdateAsync(Lead lead, string title, Guid customerId, LeadSource? source,
        double? estimatedRevenue,
        string? description, LeadStatus status)
    {
        if (CheckValidStatus(lead.Status))
        {
            Optional<bool>
                .Of(await _accountReadOnlyRepository.CheckIfExistAsync(new AccountIdSpecification(customerId)))
                .ThrowIfNotPresent(new AccountNotFoundException(customerId));
            lead.Update(title, customerId, source, estimatedRevenue, description, status);
        }
        else
        {
            lead.Description = description;
            lead.Source = source;
        }

        return lead;
    }

    public Lead UpdateStatus(Lead lead, LeadStatus status)
    {
        lead.Status = status;
        return lead;
    }

    public async Task<IList<Lead>> DeleteManyAsync(IEnumerable<Guid> ids)
    {
        List<Lead> leads = new();
        foreach (var id in ids)
        {
            var leadIdSpecification = new LeadIdSpecification(id);
            var lead = Optional<Lead>.Of(await _leadReadOnlyRepository.GetAnyAsync(leadIdSpecification))
                .ThrowIfNotPresent(new LeadNotFoundException(id)).Get();
            leads.Add(lead);
        }

        return leads;
    }

    public Lead Disqualify(Lead lead, LeadDisqualificationReason reason, string? description = null)
    {
        if (!CheckValidStatus(lead.Status)) throw new LeadValidStatusException(lead.Status);
        lead.Status = LeadStatus.Disqualified;
        lead.DisqualificationReason = reason;
        lead.DisqualificationDescription = description;
        lead.DisqualificationDate = DateTime.UtcNow;
        return lead;
    }

    public Lead Qualify(Lead lead,Guid dealId)
    {
        if (!CheckValidStatus(lead.Status)) throw new LeadValidStatusException(lead.Status);

        lead.Status = LeadStatus.Qualified;
        lead.AddDomainEvent(new QualifiedLeadDomainEvent(dealId,lead.Id, lead.CustomerId, lead.EstimatedRevenue, lead.Title));

        return lead;
    }

    private static bool CheckValidStatus(LeadStatus status)
    {
        return status is LeadStatus.Open or LeadStatus.Prospect;
    }
}