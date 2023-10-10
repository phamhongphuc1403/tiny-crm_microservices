using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.AccountDTOs;

namespace Sales.Application.CQRS.Commands.AccountCommands.Requests;

public class CreateAccountCommand : CreateAccountDto, ICommand<AccountSummaryDto>
{
    public CreateAccountCommand(CreateAccountDto dto)
    {
        Name = dto.Name;
        Email = dto.Email;
    }
}