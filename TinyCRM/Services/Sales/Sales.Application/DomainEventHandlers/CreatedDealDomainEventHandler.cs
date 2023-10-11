using BuildingBlock.Domain.DomainEvent;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Domain.DealAggregate.Events;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.DomainService;
using Sales.Domain.LeadAggregate.Enums;
using Sales.Domain.LeadAggregate.Specifications;

namespace Sales.Application.DomainEventHandlers;

public class CreatedDealDomainEventHandler : IDomainEventHandler<CreatedDealEvent>
{
    private readonly IOperationRepository<Lead> _leadOperationRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadOnlyRepository;
    private readonly ILeadDomainService _leadDomainService;
    private readonly IUnitOfWork _unitOfWork;
    public CreatedDealDomainEventHandler(IOperationRepository<Lead> leadOperationRepository,
        IReadOnlyRepository<Lead> leadReadOnlyRepository, ILeadDomainService leadDomainService, IUnitOfWork unitOfWork)
    {
        _leadOperationRepository = leadOperationRepository;
        _leadReadOnlyRepository = leadReadOnlyRepository;
        _leadDomainService = leadDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreatedDealEvent notification, CancellationToken cancellationToken)
    {
        var lead = await _leadReadOnlyRepository.GetAnyAsync(new LeadIdSpecification(notification.LeadId));
        _leadDomainService.UpdateStatus(lead!,LeadStatus.Qualified);
        _leadOperationRepository.Update(lead!);
    }
}