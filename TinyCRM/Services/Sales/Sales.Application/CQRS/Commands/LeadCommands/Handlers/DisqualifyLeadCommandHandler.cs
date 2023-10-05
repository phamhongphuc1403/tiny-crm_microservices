using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Application.CQRS.Commands.LeadCommands.Requests;
using Sales.Application.DTOs.Leads;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.DomainService;
using Sales.Domain.LeadAggregate.Exceptions;
using Sales.Domain.LeadAggregate.Specifications;

namespace Sales.Application.CQRS.Commands.LeadCommands.Handlers;

public class DisqualifyLeadCommandHandler : ICommandHandler<DisqualifyLeadCommand, LeadDetailDto>
{
    private readonly ILeadDomainService _leadDomainService;
    private readonly IOperationRepository<Lead> _leadOperationRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DisqualifyLeadCommandHandler(IUnitOfWork unitOfWork, IReadOnlyRepository<Lead> leadReadOnlyRepository,
        IOperationRepository<Lead> leadOperationRepository, ILeadDomainService leadDomainService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _leadReadOnlyRepository = leadReadOnlyRepository;
        _leadOperationRepository = leadOperationRepository;
        _leadDomainService = leadDomainService;
        _mapper = mapper;
    }

    public async Task<LeadDetailDto> Handle(DisqualifyLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = Optional<Lead>.Of(await _leadReadOnlyRepository.GetAnyAsync(new LeadIdSpecification(request.Id)))
            .ThrowIfNotPresent(new LeadNotFoundException(request.Id)).Get();
        lead = _leadDomainService.Disqualify(lead, request.DisqualificationReason,
            request.DescriptionDisqualification);
        
        _leadOperationRepository.Update(lead);

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<LeadDetailDto>(lead);
    }
}