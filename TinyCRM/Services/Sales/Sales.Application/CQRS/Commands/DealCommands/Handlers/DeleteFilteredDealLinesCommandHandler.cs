using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Commands.DealCommands.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Specifications;

namespace Sales.Application.CQRS.Commands.DealCommands.Handlers;

public class
    DeleteFilteredDealLinesCommandHandler : ICommandHandler<DeleteFilteredDealLinesCommand, DealActualRevenueDto>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IReadOnlyRepository<DealLine> _dealLineReadOnlyRepository;
    private readonly IOperationRepository<Deal> _dealOperationRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFilteredDealLinesCommandHandler(IDealDomainService dealDomainService,
        IOperationRepository<Deal> dealOperationRepository, IUnitOfWork unitOfWork,
        IReadOnlyRepository<DealLine> dealLineReadOnlyRepository, IMapper mapper)
    {
        _dealDomainService = dealDomainService;
        _dealOperationRepository = dealOperationRepository;
        _unitOfWork = unitOfWork;
        _dealLineReadOnlyRepository = dealLineReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<DealActualRevenueDto> Handle(DeleteFilteredDealLinesCommand request,
        CancellationToken cancellationToken)
    {
        var deal = await _dealDomainService.GetDealAsync(request.DealId);

        var dealLineDealIdSpecification = new DealLineDealIdSpecification(deal.Id);

        var dealLineProductCodePartialMatchSpecification =
            new DealLineProductCodePartialMatchSpecification(request.Keyword);
        var dealLineProductNamePartialMatchSpecification =
            new DealLineProductNamePartialMatchSpecification(request.Keyword);

        var specification =
            dealLineDealIdSpecification.And(
                dealLineProductCodePartialMatchSpecification.Or(dealLineProductNamePartialMatchSpecification));

        var dealLines = await _dealLineReadOnlyRepository.GetAllAsync(specification);

        _dealDomainService.RemoveDealLines(deal, dealLines);

        _dealOperationRepository.Update(deal);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<DealActualRevenueDto>(deal);
    }
}