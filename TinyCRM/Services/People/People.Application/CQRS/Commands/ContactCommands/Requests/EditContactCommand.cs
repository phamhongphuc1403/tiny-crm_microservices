using BuildingBlock.Application.CQRS.Command;
using People.Application.DTOs.ContactDTOs;

namespace People.Application.CQRS.Commands.ContactCommands.Requests;

public class EditContactCommand : CreateOrEditContactDto, ICommand<ContactDetailDto>
{
    public Guid Id { get; private init; }

    public EditContactCommand(Guid id, CreateOrEditContactDto dto)
    {
        Id = id;
        Name = dto.Name;
        Email = dto.Email;
        Phone = dto.Phone;
        AccountId = dto.AccountId;
    }
}