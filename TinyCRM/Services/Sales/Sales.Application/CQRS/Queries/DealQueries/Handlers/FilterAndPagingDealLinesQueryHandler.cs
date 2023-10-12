using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Sales.Application.CQRS.Queries.DealQueries.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate.DomainService;
using Sales.Domain.DealAggregate.Entities;
using Sales.Domain.DealAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.DealQueries.Handlers;

public class
    FilterAndPagingDealLinesQueryHandler : IQueryHandler<FilterAndPagingDealLinesQuery,
        FilterAndPagingResultDto<DealLineDto>>
{
    private readonly IDealDomainService _dealDomainService;
    private readonly IReadOnlyRepository<DealLine> _dealLineReadOnlyRepository;
    private readonly ILogger<FilterAndPagingDealLineDto> _logger;
    private readonly IMapper _mapper;

    public FilterAndPagingDealLinesQueryHandler(IDealDomainService dealDomainService, IMapper mapper,
        IReadOnlyRepository<DealLine> dealLineReadOnlyRepository, ILogger<FilterAndPagingDealLineDto> logger)
    {
        _dealDomainService = dealDomainService;
        _mapper = mapper;
        _dealLineReadOnlyRepository = dealLineReadOnlyRepository;
        _logger = logger;
    }

    public async Task<FilterAndPagingResultDto<DealLineDto>> Handle(FilterAndPagingDealLinesQuery request,
        CancellationToken cancellationToken)
    {
        var deal = await _dealDomainService.GetDealAsync(request.DealId);

        var dealLineDealIdSpecification = new DealLineDealIdSpecification(deal.Id);

        var dealLineProductNamePartialMatchSpecification =
            new DealLineProductNamePartialMatchSpecification(request.Keyword);

        var dealLineProductCodePartialMatchSpecification =
            new DealLineProductCodePartialMatchSpecification(request.Keyword);

        var dealLineKeyWordPartialMatchSpecification =
            dealLineProductCodePartialMatchSpecification.Or(dealLineProductNamePartialMatchSpecification);

        var specification = dealLineDealIdSpecification.And(dealLineKeyWordPartialMatchSpecification);

        const string includeTables = "Product";

        var (dealLines, totalCount) = await _dealLineReadOnlyRepository.GetFilterAndPagingAsync(specification,
            request.Sort, request.Skip, request.Take, includeTables);

        return new FilterAndPagingResultDto<DealLineDto>(_mapper.Map<List<DealLineDto>>(dealLines), request.Skip,
            request.Take, totalCount);
    }
}