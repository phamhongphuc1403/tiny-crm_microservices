using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Application.IntegrationEvents.Events;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Enums;
using Sales.Domain.DealAggregate.Specifications;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class DeleteFilterDealsCommandHandler : ICommandHandler<DeleteFilterDealsCommand>
{
    private readonly IOperationRepository<Deal> _leadOperationRepository;
    private readonly IReadOnlyRepository<Deal> _leadReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    public DeleteFilterDealsCommandHandler(IOperationRepository<Deal> leadOperationRepository,
        IReadOnlyRepository<Deal> leadReadOnlyRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _leadOperationRepository = leadOperationRepository;
        _leadReadOnlyRepository = leadReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task Handle(DeleteFilterDealsCommand request, CancellationToken cancellationToken)
    {
        const string includes = "Customer";
        var dealAccountNamePartialMatchSpecification =
            new DealAccountNamePartialMatchSpecification(request.Keyword);
        var dealTitlePartialMatchSpecification = new DealTitlePartialMatchSpecification(request.Keyword);
        var dealStatusFilterSpecification = new DealStatusFilterSpecification(request.Status);
        var specification =
            dealStatusFilterSpecification.And(
                dealTitlePartialMatchSpecification.Or(dealAccountNamePartialMatchSpecification));
        var deals = await _leadReadOnlyRepository.GetAllAsync(specification, includes);
        _leadOperationRepository.RemoveRange(deals);
        await _unitOfWork.SaveChangesAsync();
        foreach (var deal in deals.Where(deal => deal.DealStatus == DealStatus.Won))
        {
            _eventBus.Publish(new DeletedWonDealIntegrationEvent(deal.CustomerId, deal.ActualRevenue));
        }
    }
}