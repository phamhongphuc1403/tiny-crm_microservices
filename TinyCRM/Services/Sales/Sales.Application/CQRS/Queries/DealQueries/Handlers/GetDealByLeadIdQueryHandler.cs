using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Application.CQRS.Queries.DealQueries.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Application.DTOs.LeadDTOs;
using Sales.Domain.DealAggregate;
using Sales.Domain.DealAggregate.Exceptions;
using Sales.Domain.DealAggregate.Specifications;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Exceptions;

namespace Sales.Application.CQRS.Queries.DealQueries.Handlers;

public class GetDealByLeadIdQueryHandler : IQueryHandler<GetDealByLeadIdQuery, DealDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Deal> _repository;

    public GetDealByLeadIdQueryHandler(IMapper mapper, IReadOnlyRepository<Deal> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<DealDetailDto> Handle(GetDealByLeadIdQuery request, CancellationToken cancellationToken)
    {
        var dealLeadIdSpecification = new DealLeadIdSpecification(request.LeadId);

        var lead = Optional<Deal>.Of(await _repository.GetAnyAsync(dealLeadIdSpecification))
            .ThrowIfNotPresent(new DealNotfoundException($"Deal with LeadId {request.LeadId} not found")).Get();

        return _mapper.Map<DealDetailDto>(lead);
    }
}