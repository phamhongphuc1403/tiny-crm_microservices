using BuildingBlock.Application.CQRS.Command;
using People.Application.DTOs.AccountDTOs;

namespace People.Application.CQRS.Commands.AccountCommands.Requests;

public class DeleteFilteredAccountsCommand : FilterAccountsDto, ICommand
{
    public DeleteFilteredAccountsCommand(FilterAccountsDto dto)
    {
        Keyword = dto.Keyword;
    }
}