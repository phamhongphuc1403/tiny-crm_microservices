namespace People.Application.DTOs.AccountDTOs;

public class CreateOrEditAccountDto
{
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
}