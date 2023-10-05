using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.LeadCommands.Requests;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Specifications;

namespace Sales.Application.CQRS.Commands.LeadCommands.Handlers;

public class DeleteFilterLeadsCommandHandler : ICommandHandler<DeleteFilterLeadsCommand>
{
    private readonly IOperationRepository<Lead> _leadOperationRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFilterLeadsCommandHandler(IOperationRepository<Lead> leadOperationRepository, IUnitOfWork unitOfWork,
        IReadOnlyRepository<Lead> leadReadOnlyRepository)
    {
        _leadOperationRepository = leadOperationRepository;
        _unitOfWork = unitOfWork;
        _leadReadOnlyRepository = leadReadOnlyRepository;
    }

    public async Task Handle(DeleteFilterLeadsCommand request, CancellationToken cancellationToken)
    {
        var leadAccountNamePartialMatchSpecification = new LeadAccountNamePartialMatchSpecification(request.Keyword);
        var leadTitlePartialMatchSpecification = new LeadTitlePartialMatchSpecification(request.Keyword);
        var specification = leadTitlePartialMatchSpecification.Or(leadAccountNamePartialMatchSpecification);
        var leads = await _leadReadOnlyRepository.GetAllAsync(specification);

        _leadOperationRepository.RemoveRange(leads);
        await _unitOfWork.SaveChangesAsync();
    }
}