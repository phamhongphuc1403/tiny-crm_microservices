using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.LeadDTOs;

namespace Sales.Application.CQRS.Commands.LeadCommands.Requests;

public class DisqualifyLeadCommand : LeadDisqualifyDto, ICommand<LeadDetailDto>
{
    public DisqualifyLeadCommand(Guid id, LeadDisqualifyDto dto)
    {
        Id = id;
        DisqualificationReason = dto.DisqualificationReason;
        DescriptionDisqualification = dto.DescriptionDisqualification;
    }

    public Guid Id { get; set; }
}