using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.LeadCommands.Requests;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.DomainService;

namespace Sales.Application.CQRS.Commands.LeadCommands.Handlers;

public class DeleteManyLeadsCommandHandler : ICommandHandler<DeleteManyLeadsCommand>
{
    private readonly ILeadDomainService _leadDomainService;
    private readonly IOperationRepository<Lead> _leadOperationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteManyLeadsCommandHandler(ILeadDomainService leadDomainService,
        IOperationRepository<Lead> leadOperationRepository, IUnitOfWork unitOfWork)
    {
        _leadDomainService = leadDomainService;
        _leadOperationRepository = leadOperationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteManyLeadsCommand request, CancellationToken cancellationToken)
    {
        var leads = await _leadDomainService.DeleteManyAsync(request.Ids);
        _leadOperationRepository.RemoveRange(leads);
        await _unitOfWork.SaveChangesAsync();
    }
}