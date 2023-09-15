using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using Sales.Application.CQRS.Queries.Requests;
using Sales.Application.DTOs;
using Sales.Domain.Repositories.LeadRepository;
using Sales.Domain.Specifications;

namespace Sales.Application.CQRS.Queries.Handlers;

public class
    FilterAndPagingLeadsQueryHandler : IQueryHandler<FilterAndPagingLeadsQuery, FilterAndPagingResultDto<LeadDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeadReadOnlyRepository _repository;

    public FilterAndPagingLeadsQueryHandler(
        ILeadReadOnlyRepository repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<LeadDto>> Handle(FilterAndPagingLeadsQuery query,
        CancellationToken cancellationToken)
    {
        var leadTitleSpecification = new LeadTitleSpecification(query.Keyword);

        var leadStatusSpecification = new LeadStatusSpecification(query.Status);

        var specification = leadTitleSpecification.And(leadStatusSpecification);


        var (deals, totalCount) = await _repository.GetFilterAndPagingAsync(specification, null,
            query.Sort, query.PageIndex, query.PageSize);

        return new FilterAndPagingResultDto<LeadDto>(_mapper.Map<List<LeadDto>>(deals), query.PageIndex, query.PageSize,
            totalCount);
    }
}