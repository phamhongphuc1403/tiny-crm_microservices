using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.LeadDTOs;

namespace Sales.Application.CQRS.Commands.LeadCommands.Requests;

public class QualifyLeadCommand:ICommand<LeadDetailDto>
{
    public Guid Id { get; set; }

    public QualifyLeadCommand(Guid id)
    {
        this.Id = id;
    }
}