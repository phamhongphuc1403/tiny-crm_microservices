using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class DeleteManyDealsCommandHandler: ICommandHandler<DeleteManyDealsCommand>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteManyDealsCommandHandler(IDealDomainService dealDomainService, IOperationRepository<Deal> dealOperationRepository, IUnitOfWork unitOfWork)
    {
        _dealDomainService = dealDomainService;
        _dealOperationRepository = dealOperationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteManyDealsCommand request, CancellationToken cancellationToken)
    {
        var deals = await _dealDomainService.DeleteManyDealAsync(request.Ids);
        _dealOperationRepository.RemoveRange(deals);
        await _unitOfWork.SaveChangesAsync();
    }
}