using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Queries.DealQueries.Requests;
using Sales.Application.DTOs.DealDTOs;
using Sales.Domain.DealAggregate;
using Sales.Domain.DealAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.DealQueries.Handlers;

public class
    FilterAndPagingDealsQueryHandler : IQueryHandler<FilterAndPagingDealsQuery,
        FilterAndPagingResultDto<DealSummaryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Deal> _repository;

    public FilterAndPagingDealsQueryHandler(IMapper mapper, IReadOnlyRepository<Deal> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<FilterAndPagingResultDto<DealSummaryDto>> Handle(FilterAndPagingDealsQuery request,
        CancellationToken cancellationToken)
    {
        const string includes = "Customer";

        var dealTitleSpecification = new DealTitlePartialMatchSpecification(request.Keyword);
        var dealAccountNameSpecification = new DealAccountNamePartialMatchSpecification(request.Keyword);
        var dealStatusSpecification = new DealStatusFilterSpecification(request.Status);

        var specification = dealStatusSpecification.And(dealTitleSpecification.Or(dealAccountNameSpecification));

        var (deals, totalCount) = await _repository.GetFilterAndPagingAsync(specification,
            request.Sort, request.Skip, request.Take, includes);

        return new FilterAndPagingResultDto<DealSummaryDto>(_mapper.Map<List<DealSummaryDto>>(deals), request.Skip,
            request.Take,
            totalCount);
    }
}