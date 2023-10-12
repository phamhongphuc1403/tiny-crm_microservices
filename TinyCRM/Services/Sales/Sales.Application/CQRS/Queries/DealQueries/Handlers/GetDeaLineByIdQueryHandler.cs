using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Application.CQRS.Queries.DealQueries.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Exceptions;
using Sales.Domain.DealAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.DealQueries.Handlers;

public class GetDeaLineByIdQueryHandler : IQueryHandler<GetDeaLineByIdQuery, DealLineDto>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IReadOnlyRepository<DealLine> _deaLineReadOnlyRepository;
    private readonly IMapper _mapper;

    public GetDeaLineByIdQueryHandler(IDealDomainService dealDomainService, IMapper mapper,
        IReadOnlyRepository<DealLine> deaLineReadOnlyRepository)
    {
        _dealDomainService = dealDomainService;
        _mapper = mapper;
        _deaLineReadOnlyRepository = deaLineReadOnlyRepository;
    }

    public async Task<DealLineDto> Handle(GetDeaLineByIdQuery request, CancellationToken cancellationToken)
    {
        var deal = await _dealDomainService.GetDealAsync(request.DealId);

        var dealLineDealIdSpecification = new DealLineDealIdSpecification(deal.Id);

        var dealLineIdSpecification = new DealLineIdSpecification(request.DealLineId);

        var specification = dealLineIdSpecification.And(dealLineDealIdSpecification);

        const string includeTable = "Product";

        var dealLine = Optional<DealLine>
            .Of(await _deaLineReadOnlyRepository.GetAnyAsync(specification, includeTable))
            .ThrowIfNotPresent(new DealLineNotFoundException(request.DealLineId)).Get();

        return _mapper.Map<DealLineDto>(dealLine);
    }
}