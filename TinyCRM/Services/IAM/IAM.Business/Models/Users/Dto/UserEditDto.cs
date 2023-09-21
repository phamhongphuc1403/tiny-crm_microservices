using System.ComponentModel.DataAnnotations;

namespace IAM.Business.Models.Users.Dto;

public class UserEditDto
{
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "The Name must be between {2} and {1} characters long.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
    public string Email { get; set; } = null!;
}