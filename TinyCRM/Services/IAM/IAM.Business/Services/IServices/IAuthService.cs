using IAM.Business.Models.Dto.Users;

namespace IAM.Business.Services.IServices;

public interface IAuthService
{
    Task<string> SignInAsync(SignInDto signInDto);
}