using BuildingBlock.Application.DTOs;
using BuildingBlock.Application.Identity.Permissions;
using IAM.Business.Models.Users;
using IAM.Business.Models.Users.Dto;
using IAM.Business.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAM.API.Controllers;

[ApiController]
[Route("api/iam-accounts")]
public class IamAccountController : Controller
{
    private readonly IIamAccountService _iamAccountService;

    public IamAccountController(IIamAccountService iamAccountService)
    {
        _iamAccountService = iamAccountService;
    }

    [HttpGet]
    [Authorize(Policy = TinyCrmPermissions.Users.Read)]
    public async Task<ActionResult<FilterAndPagingResultDto<UserSummaryDto>>> GetFilteredAndPagedAsync(
        [FromQuery] FilterAndPagingUsersDto dto)
    {
        var users = await _iamAccountService.FilterAndPagingUsersAsync(dto);
        return Ok(users);
    }
}