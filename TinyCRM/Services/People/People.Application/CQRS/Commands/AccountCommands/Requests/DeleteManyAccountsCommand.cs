using BuildingBlock.Application.CQRS.Command;
using People.Application.DTOs.AccountDTOs;

namespace People.Application.CQRS.Commands.AccountCommands.Requests;

public class DeleteManyAccountsCommand : DeleteManyAccountsDto, ICommand
{
    public DeleteManyAccountsCommand(DeleteManyAccountsDto dto)
    {
        Ids = dto.Ids;
    }
}