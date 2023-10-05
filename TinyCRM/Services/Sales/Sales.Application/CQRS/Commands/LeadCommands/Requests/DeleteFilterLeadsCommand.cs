using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.Leads;

namespace Sales.Application.CQRS.Commands.LeadCommands.Requests;

public class DeleteFilterLeadsCommand : FilterLeadsDto, ICommand
{
    public DeleteFilterLeadsCommand(FilterLeadsDto dto)
    {
        Keyword = dto.Keyword;
    }
}