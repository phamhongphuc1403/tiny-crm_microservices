using MediatR;
using Sales.Application.DTOs.DealDTOs;

namespace Sales.Application.CQRS.Commands.DealCommands.Requests;

public class CreateDealCommand : DealCreateDto, IRequest<DealDetailDto>
{
    public CreateDealCommand(DealCreateDto dto)
    {
        Title = dto.Title;
        CustomerId = dto.CustomerId;
        LeadId = dto.LeadId;
        Description = dto.Description;
        EstimatedRevenue = dto.EstimatedRevenue;
        ActualRevenue = dto.ActualRevenue;
    }
}