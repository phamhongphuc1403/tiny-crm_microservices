using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Domain.LeadAggregate.DomainService;

public interface ILeadDomainService
{
    Task<Lead> CreateAsync(string title, Guid customerId, LeadSource? source, double? estimatedRevenue,
        string? description);

    Task<Lead> UpdateAsync(Lead lead, string title, Guid customerId, LeadSource? source, double? estimatedRevenue,
        string? description, LeadStatus status);

    Task<IList<Lead>> DeleteManyAsync(IEnumerable<Guid> ids);

    Lead Disqualify(Lead lead, LeadDisqualificationReason reason, string? description = null);
    Lead Qualify(Lead lead);
}