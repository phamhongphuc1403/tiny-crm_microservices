using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Repositories;
using Sales.Application.CQRS.Queries.LeadQueries.Requests;
using Sales.Application.DTOs.LeadDTOs;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Enums;
using Sales.Domain.LeadAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.LeadQueries.Handlers;

public class
    GetLeadsValidByAccountIdQueryHandler : IQueryHandler<GetLeadsValidByAccountIdQuery, FilterAndPagingResultDto<LeadSummaryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Lead> _repository;

    public GetLeadsValidByAccountIdQueryHandler(IMapper mapper, IReadOnlyRepository<Lead> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<FilterAndPagingResultDto<LeadSummaryDto>> Handle(GetLeadsValidByAccountIdQuery request,
        CancellationToken cancellationToken)
    {
        const string includes = "Customer";

        var leadTitleSpecification = new LeadTitlePartialMatchSpecification(request.Keyword);
        var leadAccountIdSpecification = new LeadAccountIdMatchSpecification(request.AccountId);

        var leadStatusSpecification =
            new LeadStatusFilterSpecification(LeadStatus.Open).Or(
                new LeadStatusFilterSpecification(LeadStatus.Prospect));


        var specification = leadAccountIdSpecification.And(leadTitleSpecification).And(leadStatusSpecification);
        var (leads, totalCount) = await _repository.GetFilterAndPagingAsync(specification,
            request.Sort, request.Skip, request.Take, includes);

        return new FilterAndPagingResultDto<LeadSummaryDto>(_mapper.Map<List<LeadSummaryDto>>(leads), totalCount);
    }
}