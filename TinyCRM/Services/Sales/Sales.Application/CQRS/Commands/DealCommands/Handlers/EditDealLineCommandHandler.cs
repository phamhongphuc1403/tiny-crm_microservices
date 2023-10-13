using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class EditDealLineCommandHandler : ICommandHandler<EditDealLineCommand, DealLineWithDealActualRevenueDto>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public EditDealLineCommandHandler(IDealDomainService dealDomainService,
        IOperationRepository<Deal> dealOperationRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _dealDomainService = dealDomainService;
        _dealOperationRepository = dealOperationRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DealLineWithDealActualRevenueDto> Handle(EditDealLineCommand request,
        CancellationToken cancellationToken)
    {
        var deal = await _dealDomainService.GetDealAsync(request.DealId);

        var dealLine = await _dealDomainService.EditDealLineAsync(deal, request.DealLineId, request.ProductId,
            request.Quantity, request.PricePerUnit);

        _dealOperationRepository.Update(deal);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<DealLineWithDealActualRevenueDto>(dealLine);
    }
}