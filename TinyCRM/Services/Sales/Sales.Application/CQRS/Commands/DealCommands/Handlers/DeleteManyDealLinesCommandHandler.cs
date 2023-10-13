using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class DeleteManyDealLinesCommandHandler : ICommandHandler<DeleteManyDealLinesCommand, DealActualRevenueDto>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteManyDealLinesCommandHandler(IDealDomainService dealDomainService,
        IOperationRepository<Deal> dealOperationRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _dealDomainService = dealDomainService;
        _dealOperationRepository = dealOperationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DealActualRevenueDto> Handle(DeleteManyDealLinesCommand request,
        CancellationToken cancellationToken)
    {
        var deal = await _dealDomainService.GetDealAsync(request.DealId);

        _dealDomainService.RemoveDealLines(deal, request.DealLineIds);

        _dealOperationRepository.Update(deal);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<DealActualRevenueDto>(deal);
    }
}