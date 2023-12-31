using BuildingBlock.Application.CQRS.Command;
using People.Application.DTOs.AccountDTOs;

namespace People.Application.CQRS.Commands.AccountCommands.Requests;

public class EditAccountCommand : CreateOrEditAccountDto, ICommand<AccountDetailDto>
{
    public EditAccountCommand(Guid id, CreateOrEditAccountDto dto)
    {
        Id = id;
        Name = dto.Name;
        Email = dto.Email;
        Phone = dto.Phone;
        Address = dto.Address;
    }

    public Guid Id { get; private init; }
}