using BuildingBlock.Application.DTOs;
using IAM.Business.Models.Users;
using IAM.Business.Models.Users.Dto;

namespace IAM.Business.Services.IServices;

public interface IIamAccountService
{
    Task<FilterAndPagingResultDto<UserSummaryDto>> FilterAndPagingUsersAsync(FilterAndPagingUsersDto filterAndPagingUsersDto);
    Task<UserDetailDto> CreateUserAsync(UserCreateDto userCreateDto);
    Task<UserDetailDto> GetDetailUserAsync(Guid id);
    Task ChangePasswordAsync(Guid id, UserChangePasswordDto userChangePasswordDto);
    Task<UserDetailDto> UpdateUserAsync(Guid id, UserEditDto userEditDto);
    Task DeleteUserAsync(DeleteManyUsersDto deleteManyUsersDto);
}