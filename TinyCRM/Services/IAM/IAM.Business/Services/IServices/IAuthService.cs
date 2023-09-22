using IAM.Business.Models.Users.Dto;

namespace IAM.Business.Services.IServices;

public interface IAuthService
{
    Task<string> SignInAsync(SignInDto signInDto);
    Task<IEnumerable<string>> GetPermissionsAsync(string userId);
}