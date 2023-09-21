using BuildingBlock.Application.DTOs;
using IAM.Business.Models.Users;
using IAM.Business.Models.Users.Dto;

namespace IAM.Business.Services.IServices;

public interface IIamAccountService
{
    Task<FilterAndPagingResultDto<UserSummaryDto>> FilterAndPagingUsersAsync(FilterAndPagingUsersDto filterAndPagingUsersDto);
}