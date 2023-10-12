using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using Sales.Application.CQRS.Queries.DealQueries.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.DomainService;

namespace Sales.Application.CQRS.Queries.DealQueries.Handlers;

public class GetDeaLineByIdQueryHandler : IQueryHandler<GetDeaLineByIdQuery, DealLineDto>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IMapper _mapper;

    public GetDeaLineByIdQueryHandler(IDealDomainService dealDomainService, IMapper mapper)
    {
        _dealDomainService = dealDomainService;
        _mapper = mapper;
    }

    public async Task<DealLineDto> Handle(GetDeaLineByIdQuery request, CancellationToken cancellationToken)
    {
        var deal = await _dealDomainService.GetDealAsync(request.DealId);

        var dealLine = deal.GetDealLine(request.DealLineId);

        return _mapper.Map<DealLineDto>(dealLine);
    }
}