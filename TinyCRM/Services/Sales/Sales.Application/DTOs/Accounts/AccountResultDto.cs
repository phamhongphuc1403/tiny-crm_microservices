namespace Sales.Application.DTOs.Accounts;

public class AccountResultDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
}