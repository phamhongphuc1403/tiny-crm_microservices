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

public class EditLeadCommandHandler : ICommandHandler<EditLeadCommand, LeadDetailDto>
{
    private readonly ILeadDomainService _leadDomainService;
    private readonly IOperationRepository<Lead> _leadOperationRepository;
    private readonly IReadOnlyRepository<Lead> _leadReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public EditLeadCommandHandler(ILeadDomainService leadDomainService, IUnitOfWork unitOfWork,
        IOperationRepository<Lead> leadOperationRepository, IMapper mapper,
        IReadOnlyRepository<Lead> leadReadOnlyRepository)
    {
        _leadDomainService = leadDomainService;
        _unitOfWork = unitOfWork;
        _leadOperationRepository = leadOperationRepository;
        _mapper = mapper;
        _leadReadOnlyRepository = leadReadOnlyRepository;
    }

    public async Task<LeadDetailDto> Handle(EditLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = Optional<Lead>.Of(await _leadReadOnlyRepository.GetAnyAsync(new LeadIdSpecification(request.Id)))
            .ThrowIfNotPresent(new LeadNotFoundException(request.Id)).Get();
        lead = await _leadDomainService.UpdateAsync(lead, request.Title, request.CustomerId, request.Source,
            request.EstimatedRevenue, request.Description, request.Status);
        _leadOperationRepository.Update(lead);

        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<LeadDetailDto>(lead);
    }
}