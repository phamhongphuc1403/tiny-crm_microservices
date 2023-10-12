using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Application.CQRS.Queries.DealQueries.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Exceptions;
using Sales.Domain.DealAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.DealQueries.Handlers;

public class GetDealByDealIdQueryHandler : IQueryHandler<GetDealByDealIdQuery, DealDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Deal> _repository;

    public GetDealByDealIdQueryHandler(IMapper mapper, IReadOnlyRepository<Deal> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<DealDetailDto> Handle(GetDealByDealIdQuery request, CancellationToken cancellationToken)
    {
        var dealDealIdSpecification = new DealIdSpecification(request.DealId);

        var deal = Optional<Deal>.Of(await _repository.GetAnyAsync(dealDealIdSpecification))
            .ThrowIfNotPresent(new DealNotfoundException(request.DealId)).Get();

        return _mapper.Map<DealDetailDto>(deal);
    }
}