using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Queries.LeadQueries.Requests;
using Sales.Application.DTOs.Leads;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.LeadQueries.Handlers;

public class
    FilterAndPagingLeadsQueryHandler : IQueryHandler<FilterAndPagingLeadsQuery, FilterAndPagingResultDto<LeadSummaryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Lead> _repository;

    public FilterAndPagingLeadsQueryHandler(
        IReadOnlyRepository<Lead> repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<LeadSummaryDto>> Handle(FilterAndPagingLeadsQuery query,
        CancellationToken cancellationToken)
    {
        var includes = "Customer";
        var leadTitleSpecification = new LeadTitlePartialMatchSpecification(query.Keyword);
        var leadAccountNameSpecification = new LeadAccountNamePartialMatchSpecification(query.Keyword);
        var leadStatusSpecification = new LeadStatusFilterSpecification(query.Status);

        var specification = leadStatusSpecification.And(leadTitleSpecification.Or(leadAccountNameSpecification));

        var (leads, totalCount) = await _repository.GetFilterAndPagingAsync(specification,
            query.Sort, query.Skip, query.Take,includes);

        return new FilterAndPagingResultDto<LeadSummaryDto>(_mapper.Map<List<LeadSummaryDto>>(leads), query.Skip, query.Take,
            totalCount);
    }
}