namespace People.Application.DTOs.ContactDTOs;

public class CreateOrEditContactDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public Guid AccountId { get; set; }
}