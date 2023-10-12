using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class CreateDealCommandHandler : ICommandHandler<CreateDealCommand, DealDetailDto>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDealCommandHandler(IDealDomainService dealDomainService,
        IOperationRepository<Deal> dealOperationRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _dealDomainService = dealDomainService;
        _dealOperationRepository = dealOperationRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DealDetailDto> Handle(CreateDealCommand request, CancellationToken cancellationToken)
    {
        var deal = await _dealDomainService.CreateDealAsync(request.Title, request.CustomerId, request.LeadId,
            request.Description, request.EstimatedRevenue, request.ActualRevenue);
        await _dealOperationRepository.AddAsync(deal);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<DealDetailDto>(deal);
    }
}