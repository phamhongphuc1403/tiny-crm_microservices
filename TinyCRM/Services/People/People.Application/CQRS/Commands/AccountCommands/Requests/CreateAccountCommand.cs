using BuildingBlock.Application.CQRS.Command;
using People.Application.DTOs.AccountDTOs;

namespace People.Application.CQRS.Commands.AccountCommands.Requests;

public class CreateAccountCommand : CreateOrEditAccountDto, ICommand<AccountDetailDto>
{
    public CreateAccountCommand(CreateOrEditAccountDto dto)
    {
        Name = dto.Name;
        Email = dto.Email;
        Phone = dto.Phone;
        Address = dto.Address;
    }
}