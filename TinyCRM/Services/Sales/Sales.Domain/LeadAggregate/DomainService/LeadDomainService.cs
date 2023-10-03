using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Domain.AccountAggregate;
using Sales.Domain.AccountAggregate.Exceptions;
using Sales.Domain.AccountAggregate.Specifications;
using Sales.Domain.LeadAggregate.Enums;
using Sales.Domain.LeadAggregate.Exceptions;
using Sales.Domain.LeadAggregate.Specifications;

namespace Sales.Domain.LeadAggregate.DomainService;

public class LeadDomainService : ILeadDomainService
{
    private readonly IReadOnlyRepository<Lead> _leadReadOnlyRepository;
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;

    public LeadDomainService(IReadOnlyRepository<Lead> readOnlyRepository,
        IReadOnlyRepository<Account> accountReadOnlyRepository, IReadOnlyRepository<Lead> leadReadOnlyRepository)
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
        CheckValidStatus(lead.Status);
        CheckValidStatus(status);
        Optional<bool>
            .Of(await _accountReadOnlyRepository.CheckIfExistAsync(new AccountIdSpecification(customerId)))
            .ThrowIfNotPresent(new AccountNotFoundException(customerId));
        lead.Update(title, customerId, source, estimatedRevenue, description, status);
        return lead;
    }

    private static void CheckValidStatus(LeadStatus status)
    {
        if (status is LeadStatus.Open or LeadStatus.Prospect)
            return;
        throw new LeadValidStatusException(status);
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
        CheckValidStatus(lead.Status);
        
        lead.Status = LeadStatus.Disqualify;
        lead.DisqualificationReason = reason;
        lead.Description = description;
        
        return lead;
    }

    public Lead Qualify(Lead lead)
    {
        CheckValidStatus(lead.Status);
        
        lead.Status = LeadStatus.Qualify;
        
        return lead;
    }
}