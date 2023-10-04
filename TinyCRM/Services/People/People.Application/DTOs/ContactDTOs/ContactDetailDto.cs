namespace People.Application.DTOs.ContactDTOs;

public class ContactDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public Guid AccountId { get; set; }
    public string AccountName { get; set; } = null!;
}