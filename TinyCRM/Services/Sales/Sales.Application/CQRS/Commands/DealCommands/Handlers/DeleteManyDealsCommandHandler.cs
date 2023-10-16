using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Application.IntegrationEvents.Events;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Enums;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class DeleteManyDealsCommandHandler: ICommandHandler<DeleteManyDealsCommand>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public DeleteManyDealsCommandHandler(IDealDomainService dealDomainService, IOperationRepository<Deal> dealOperationRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _dealDomainService = dealDomainService;
        _dealOperationRepository = dealOperationRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task Handle(DeleteManyDealsCommand request, CancellationToken cancellationToken)
    {
        var deals = await _dealDomainService.DeleteManyDealAsync(request.Ids);
        _dealOperationRepository.RemoveRange(deals);
        await _unitOfWork.SaveChangesAsync();

        foreach (var deal in deals.Where(deal=>deal.DealStatus == DealStatus.Won))
        {
            _eventBus.Publish(new DeletedWonDealIntegrationEvent(deal.CustomerId, deal.ActualRevenue));
        }
    }
}