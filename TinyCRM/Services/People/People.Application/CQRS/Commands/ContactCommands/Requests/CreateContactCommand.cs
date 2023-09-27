using BuildingBlock.Application.CQRS.Command;
using People.Application.DTOs.ContactDTOs;

namespace People.Application.CQRS.Commands.ContactCommands.Requests;

public class CreateContactCommand : CreateOrEditContactDto, ICommand<ContactDetailDto>
{
    public CreateContactCommand(CreateOrEditContactDto dto)
    {
        Name = dto.Name;
        Email = dto.Email;
        Phone = dto.Phone;
        AccountId = dto.AccountId;
    }
}