using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.Leads;

namespace Sales.Application.CQRS.Commands.LeadCommands.Requests;

public class DeleteManyLeadsCommand : LeadDeleteManyDto, ICommand
{
    public DeleteManyLeadsCommand(LeadDeleteManyDto dto)
    {
        Ids = dto.Ids;
    }
}