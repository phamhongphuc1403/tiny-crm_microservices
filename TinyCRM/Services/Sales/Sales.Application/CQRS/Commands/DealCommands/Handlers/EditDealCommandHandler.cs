using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Exceptions;
using Sales.Domain.DealAggregate.Specifications;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class EditDealCommandHandler : ICommandHandler<EditDealCommand, DealDetailDto>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IReadOnlyRepository<Deal> _dealReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public EditDealCommandHandler(IDealDomainService dealDomainService,
        IOperationRepository<Deal> dealOperationRepository, IMapper mapper, IUnitOfWork unitOfWork,
        IReadOnlyRepository<Deal> dealReadOnlyRepository)
    {
        _dealDomainService = dealDomainService;
        _dealOperationRepository = dealOperationRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _dealReadOnlyRepository = dealReadOnlyRepository;
    }

    public async Task<DealDetailDto> Handle(EditDealCommand request, CancellationToken cancellationToken)
    {
        var dealIdSpecification = new DealIdSpecification(request.Id);
        var deal = Optional<Deal>.Of(await _dealReadOnlyRepository.GetAnyAsync(dealIdSpecification))
            .ThrowIfNotPresent(new DealNotfoundException(request.Id)).Get();
        
        deal = await _dealDomainService.UpdateDealAsync(deal, request.Title, request.CustomerId, request.LeadId,
            request.Description, request.EstimatedRevenue, request.ActualRevenue);

        _dealOperationRepository.Update(deal);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<DealDetailDto>(deal);
    }
}