using BuildingBlock.Domain.DomainEvent;
using BuildingBlock.Domain.Repositories;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Specifications;
using Sales.Domain.LeadAggregate.Events;

namespace Sales.Application.DomainEventHandlers;

public class DeletedLeadDomainEventHandler : IDomainEventHandler<DeletedLeadDomainEvent>
{
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IReadOnlyRepository<Deal> _dealOnlyRepository;

    public DeletedLeadDomainEventHandler(IOperationRepository<Deal> dealOperationRepository,
        IReadOnlyRepository<Deal> dealOnlyRepository)
    {
        _dealOperationRepository = dealOperationRepository;
        _dealOnlyRepository = dealOnlyRepository;
    }

    public async Task Handle(DeletedLeadDomainEvent notification, CancellationToken cancellationToken)
    {
        var deal = await _dealOnlyRepository.GetAnyAsync(new DealLeadIdSpecification(notification.LeadId));
        if (deal != null)
            _dealOperationRepository.Remove(deal);
    }
}