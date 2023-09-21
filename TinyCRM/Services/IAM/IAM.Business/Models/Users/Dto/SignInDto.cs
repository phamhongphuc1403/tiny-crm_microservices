using System.ComponentModel.DataAnnotations;

namespace IAM.Business.Models.Users.Dto;

public class SignInDto
{
    [Required] public string Email { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;
}