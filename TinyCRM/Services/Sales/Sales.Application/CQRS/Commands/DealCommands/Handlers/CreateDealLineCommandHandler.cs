using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class CreateDealLineCommandHandler : ICommandHandler<CreateDealLineCommand, DealLineDto>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDealLineCommandHandler(IMapper mapper, IOperationRepository<Deal> dealOperationRepository,
        IUnitOfWork unitOfWork, IDealDomainService dealDomainService)
    {
        _mapper = mapper;
        _dealOperationRepository = dealOperationRepository;
        _unitOfWork = unitOfWork;
        _dealDomainService = dealDomainService;
    }

    public async Task<DealLineDto> Handle(CreateDealLineCommand request, CancellationToken cancellationToken)
    {
        var deal = await _dealDomainService.GetDealAsync(request.DealId);

        var dealLine = await _dealDomainService.CreateDealLineAsync(deal, request.ProductId, request.PricePerUnit,
            request.Quantity);

        _dealOperationRepository.Update(deal);

        await _unitOfWork.SaveChangesAsync();

        Console.WriteLine(deal);

        return _mapper.Map<DealLineDto>(dealLine);
    }
}