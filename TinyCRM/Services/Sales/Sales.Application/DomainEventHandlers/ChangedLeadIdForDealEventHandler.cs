using BuildingBlock.Domain.DomainEvent;
using BuildingBlock.Domain.Repositories;
using Sales.Domain.DealAggregate.Events;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.DomainService;
using Sales.Domain.LeadAggregate.Enums;
using Sales.Domain.LeadAggregate.Specifications;

namespace Sales.Application.DomainEventHandlers;

public class ChangedLeadIdForDealEventHandler : IDomainEventHandler<ChangedLeadIdForDealEvent>
{
    private readonly ILeadDomainService _leadDomainService;
    private readonly IOperationRepository<Lead> _leadOperationRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadOnlyRepository;

    public ChangedLeadIdForDealEventHandler(ILeadDomainService leadDomainService,
        IOperationRepository<Lead> leadOperationRepository, IReadOnlyRepository<Lead> leadReadOnlyRepository)
    {
        _leadDomainService = leadDomainService;
        _leadOperationRepository = leadOperationRepository;
        _leadReadOnlyRepository = leadReadOnlyRepository;
    }

    public async Task Handle(ChangedLeadIdForDealEvent notification, CancellationToken cancellationToken)
    {
        if (notification.LeadOldId != null)
        {
            var oldLead =
                await _leadReadOnlyRepository.GetAnyAsync(new LeadIdSpecification(notification.LeadOldId.Value));
            oldLead = _leadDomainService.UpdateStatus(oldLead!, LeadStatus.Open);
            _leadOperationRepository.Update(oldLead);
        }

        var newLead = await _leadReadOnlyRepository.GetAnyAsync(new LeadIdSpecification(notification.LeadNewId));
        newLead = _leadDomainService.UpdateStatus(newLead!, LeadStatus.Qualified);
        _leadOperationRepository.Update(newLead);
    }
}