namespace People.Application.DTOs;

public class CreateAccountDto
{
    public string? Name { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
}