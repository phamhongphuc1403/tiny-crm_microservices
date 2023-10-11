using BuildingBlock.Domain.DomainEvent;
using BuildingBlock.Domain.Repositories;
using Sales.Domain.DealAggregate;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.LeadAggregate.Events;

namespace Sales.Application.DomainEventHandlers;

public class QualifiedLeadDomainEventHandler : IDomainEventHandler<QualifiedLeadDomainEvent>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;

    public QualifiedLeadDomainEventHandler(IDealDomainService dealDomainService,
        IOperationRepository<Deal> dealOperationRepository)
    {
        _dealDomainService = dealDomainService;
        _dealOperationRepository = dealOperationRepository;
    }

    public async Task Handle(QualifiedLeadDomainEvent notification, CancellationToken cancellationToken)
    {
        var deal = await _dealDomainService.CreateDealAsync(notification.DealId, notification.Title,
            notification.CustomerId, notification.LeadId,
            string.Empty, notification.EstimatedRevenue, 0);
        await _dealOperationRepository.AddAsync(deal);
    }
}