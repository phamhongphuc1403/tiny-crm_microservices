using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.LeadDTOs;

namespace Sales.Application.CQRS.Commands.LeadCommands.Requests;

public class DeleteManyLeadsCommand : LeadDeleteManyDto, ICommand
{
    public DeleteManyLeadsCommand(LeadDeleteManyDto dto)
    {
        Ids = dto.Ids;
    }
}