using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.Leads;

namespace Sales.Application.CQRS.Commands.LeadCommands.Requests;

public class EditLeadCommand : LeadEditDto, ICommand<LeadDetailDto>
{
    public EditLeadCommand(Guid id, LeadEditDto dto)
    {
        Id = id;
        Title = dto.Title;
        CustomerId = dto.CustomerId;
        Description = dto.Description;
        Status = dto.Status;
        Source = dto.Source;
        EstimatedRevenue = dto.EstimatedRevenue;
    }

    public Guid Id { get; set; }
}