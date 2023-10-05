using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.Leads;

namespace Sales.Application.CQRS.Commands.LeadCommands.Requests;

public class DisqualifyLeadCommand : LeadDisqualifyDto, ICommand<LeadDetailDto>
{
    public Guid Id { get; set; }

    public DisqualifyLeadCommand(Guid id,LeadDisqualifyDto dto)
    {
        Id = id;
        DisqualificationReason = dto.DisqualificationReason;
        DescriptionDisqualification = dto.DescriptionDisqualification;
    }
}