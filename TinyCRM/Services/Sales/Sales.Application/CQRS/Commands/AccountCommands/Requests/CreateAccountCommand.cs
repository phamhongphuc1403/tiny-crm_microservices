using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.Accounts;

namespace Sales.Application.CQRS.Commands.AccountCommands.Requests;

public class CreateAccountCommand : CreateAccountDto, ICommand<AccountResultDto>
{
    public CreateAccountCommand(CreateAccountDto dto)
    {
        Name = dto.Name;
        Email = dto.Email;
    }
}