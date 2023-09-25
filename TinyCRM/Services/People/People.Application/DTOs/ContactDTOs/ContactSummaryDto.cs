namespace People.Application.DTOs.ContactDTOs;

public class ContactSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    
}