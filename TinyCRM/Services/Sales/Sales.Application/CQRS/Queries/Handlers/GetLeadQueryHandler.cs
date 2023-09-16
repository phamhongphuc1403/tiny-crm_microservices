using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Application.Utils;
using Sales.Application.CQRS.Queries.Requests;
using Sales.Application.DTOs;
using Sales.Domain.Entities;
using Sales.Domain.Exceptions;
using Sales.Domain.Repositories.LeadRepository;
using Sales.Domain.Specifications;

namespace Sales.Application.CQRS.Queries.Handlers;

public class GetLeadQueryHandler : IQueryHandler<GetLeadQuery, LeadDto>
{
    private readonly IMapper _mapper;
    private readonly ILeadReadOnlyRepository _repository;

    public GetLeadQueryHandler(
        ILeadReadOnlyRepository leadReadOnlyRepository,
        IMapper mapper
    )
    {
        _repository = leadReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<LeadDto> Handle(GetLeadQuery request, CancellationToken cancellationToken)
    {
        var leadIdSpecification = new LeadIdSpecification(request.Id);

        var lead = Optional<Lead>.Of(await _repository.GetAnyAsync(leadIdSpecification))
            .ThrowIfNotPresent(new LeadNotFoundException(request.Id)).Get();

        return _mapper.Map<LeadDto>(lead);
    }
}