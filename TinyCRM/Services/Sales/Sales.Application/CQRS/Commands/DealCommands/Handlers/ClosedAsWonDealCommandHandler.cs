using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Application.IntegrationEvents.Events;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Enums;
using Sales.Domain.DealAggregate.Exceptions;
using Sales.Domain.DealAggregate.Specifications;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class ClosedAsWonDealCommandHandler : ICommandHandler<CloseAsWonDealCommand, DealDetailDto>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IReadOnlyRepository<Deal> _dealReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    public ClosedAsWonDealCommandHandler(IDealDomainService dealDomainService,
        IOperationRepository<Deal> dealOperationRepository, IReadOnlyRepository<Deal> dealReadOnlyRepository,
        IMapper mapper, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _dealDomainService = dealDomainService;
        _dealOperationRepository = dealOperationRepository;
        _dealReadOnlyRepository = dealReadOnlyRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<DealDetailDto> Handle(CloseAsWonDealCommand request, CancellationToken cancellationToken)
    {
        var dealIdSpecification = new DealIdSpecification(request.DealId);
        var deal = Optional<Deal>.Of(await _dealReadOnlyRepository.GetAnyAsync(dealIdSpecification))
            .ThrowIfNotPresent(new DealNotfoundException(request.DealId)).Get();

        deal = _dealDomainService.UpdateStatus(deal, DealStatus.Won);
        _dealOperationRepository.Update(deal);
        await _unitOfWork.SaveChangesAsync();

        _eventBus.Publish(new ClosedWonDealIntegrationEvent(deal.CustomerId, deal.ActualRevenue));
        return _mapper.Map<DealDetailDto>(deal);
    }
}