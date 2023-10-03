using BuildingBlock.Application.CQRS.Command;
using People.Application.DTOs.ContactDTOs;

namespace People.Application.CQRS.Commands.ContactCommands.Requests;

public class DeleteFilteredContactsCommand : FilterContactsDto, ICommand
{
    public DeleteFilteredContactsCommand(FilterContactsDto dto)
    {
        Keyword = dto.Keyword;
    }
}