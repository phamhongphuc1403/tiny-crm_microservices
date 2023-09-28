using BuildingBlock.Application.CQRS.Command;
using People.Application.DTOs.ContactDTOs;

namespace People.Application.CQRS.Commands.ContactCommands.Requests;

public class DeleteManyContactsCommand : DeleteManyContactsDto, ICommand
{
    public DeleteManyContactsCommand(DeleteManyContactsDto dto)
    {
        Ids = dto.Ids;
    }
}