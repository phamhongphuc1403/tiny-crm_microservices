namespace People.Application.DTOs.ContactDTOs;

public class DeleteManyContactsDto
{
    public IEnumerable<Guid> Ids { get; set; } = null!;
}