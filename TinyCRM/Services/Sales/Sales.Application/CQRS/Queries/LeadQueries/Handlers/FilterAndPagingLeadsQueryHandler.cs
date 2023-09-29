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
    FilterAndPagingLeadsQueryHandler : IQueryHandler<FilterAndPagingLeadsQuery, FilterAndPagingResultDto<LeadDto>>
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

    public async Task<FilterAndPagingResultDto<LeadDto>> Handle(FilterAndPagingLeadsQuery query,
        CancellationToken cancellationToken)
    {
        var leadTitleSpecification = new LeadTitlePartialMatchSpecification(query.Keyword);

        var leadStatusSpecification = new LeadStatusSpecification(query.Status);

        var specification = leadTitleSpecification.And(leadStatusSpecification);


        var (deals, totalCount) = await _repository.GetFilterAndPagingAsync(specification,
            query.Sort, query.Skip, query.Take);

        return new FilterAndPagingResultDto<LeadDto>(_mapper.Map<List<LeadDto>>(deals), query.Skip, query.Take,
            totalCount);
    }
}