using BuildingBlock.Application.CQRS.Command;
using People.Application.DTOs;

namespace People.Application.CQRS.Commands.Requests;

public class DeleteManyAccountsCommand : DeleteManyAccountsDto, ICommand
{
    public DeleteManyAccountsCommand(DeleteManyAccountsDto dto)
    {
        Ids = dto.Ids;
    }
}