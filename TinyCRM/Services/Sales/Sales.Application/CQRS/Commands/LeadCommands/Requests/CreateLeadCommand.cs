using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.LeadDTOs;

namespace Sales.Application.CQRS.Commands.LeadCommands.Requests;

public class CreateLeadCommand : LeadCreateDto, ICommand<LeadDetailDto>
{
    public CreateLeadCommand(LeadCreateDto dto)
    {
        Title = dto.Title;
        CustomerId = dto.CustomerId;
        Source = dto.Source;
        EstimatedRevenue = dto.EstimatedRevenue;
        Description = dto.Description;
    }
}