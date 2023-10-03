using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.LeadCommands.Requests;
using Sales.Application.DTOs.Leads;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.DomainService;

namespace Sales.Application.CQRS.Commands.LeadCommands.Handlers;

public class CreateLeadCommandHandler : ICommandHandler<CreateLeadCommand, LeadDetailDto>
{
    private readonly ILeadDomainService _leadDomainService;
    private readonly IOperationRepository<Lead> _leadOperationRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public CreateLeadCommandHandler(ILeadDomainService leadDomainService, IUnitOfWork unitOfWork,
        IOperationRepository<Lead> leadOperationRepository,
        IMapper mapper)
    {
        _leadDomainService = leadDomainService;
        _unitOfWork = unitOfWork;
        _leadOperationRepository = leadOperationRepository;
        _mapper = mapper;
    }

    public async Task<LeadDetailDto> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = await _leadDomainService.CreateAsync(request.Title, request.CustomerId, request.Source,
            request.EstimatedRevenue, request.Description);
        await _leadOperationRepository.AddAsync(lead);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<LeadDetailDto>(lead);
    }
}