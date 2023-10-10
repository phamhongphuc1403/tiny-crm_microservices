namespace Sales.Application.DTOs.AccountDTOs;

public class AccountSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
}