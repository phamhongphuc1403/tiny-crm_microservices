using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class DeleteManyDealLinesCommandHandler : ICommandHandler<DeleteManyDealLinesCommand>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteManyDealLinesCommandHandler(IDealDomainService dealDomainService,
        IOperationRepository<Deal> dealOperationRepository, IUnitOfWork unitOfWork)
    {
        _dealDomainService = dealDomainService;
        _dealOperationRepository = dealOperationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteManyDealLinesCommand request, CancellationToken cancellationToken)
    {
        var deal = await _dealDomainService.GetDealAsync(request.DealId);

        _dealDomainService.RemoveDealLines(deal, request.DealLineIds);

        _dealOperationRepository.Update(deal);

        await _unitOfWork.SaveChangesAsync();
    }
}