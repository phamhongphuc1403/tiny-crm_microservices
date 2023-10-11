using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Application.CQRS.Commands.LeadCommands.Requests;
using Sales.Application.DTOs.LeadDTOs;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.DomainService;
using Sales.Domain.LeadAggregate.Exceptions;
using Sales.Domain.LeadAggregate.Specifications;

namespace Sales.Application.CQRS.Commands.LeadCommands.Handlers;

public class QualifyLeadCommandHandler : ICommandHandler<QualifyLeadCommand, LeadDetailDto>
{
    private readonly ILeadDomainService _leadDomainService;
    private readonly IOperationRepository<Lead> _leadOperationRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public QualifyLeadCommandHandler(IReadOnlyRepository<Lead> leadReadOnlyRepository,
        IOperationRepository<Lead> leadOperationRepository, ILeadDomainService leadDomainService, IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _leadReadOnlyRepository = leadReadOnlyRepository;
        _leadOperationRepository = leadOperationRepository;
        _leadDomainService = leadDomainService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<LeadDetailDto> Handle(QualifyLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = Optional<Lead>.Of(await _leadReadOnlyRepository.GetAnyAsync(new LeadIdSpecification(request.Id)))
            .ThrowIfNotPresent(new LeadNotFoundException(request.Id)).Get();
        var dealId = Guid.NewGuid();
        lead = _leadDomainService.Qualify(lead, dealId);
        _leadOperationRepository.Update(lead);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<LeadDetailDto>(lead);
    }
}