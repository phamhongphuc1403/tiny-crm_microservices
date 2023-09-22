using System.ComponentModel.DataAnnotations;

namespace IAM.Business.Models.Users.Dto;

public class UserCreateDto
{
    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "The Name must be between {2} and {1} characters long.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "The Password field is required.")]
    [MinLength(6, ErrorMessage = "The Password must be at least {1} characters long.")]
    [MaxLength(255, ErrorMessage = "The Password must not exceed {1} characters.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{6,}$",
        ErrorMessage =
            "The Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character (@#$%^&+=), and be at least 6 characters long.")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "The Confirm Password field is required.")]
    [Compare("Password", ErrorMessage = "The Confirm Password does not match the Password.")]
    public string ConfirmPassword { get; set; } = null!;
}