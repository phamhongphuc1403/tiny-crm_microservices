namespace IAM.Business.Models.Users.Dto;

public class UserSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}