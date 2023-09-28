using BuildingBlock.Application.DTOs;
using BuildingBlock.Application.Identity.Permissions;
using IAM.Business.Models.Users;
using IAM.Business.Models.Users.Dto;
using IAM.Business.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAM.API.Controllers;

[ApiController]
[Route("api/v1/iam-accounts/")]
public class IamAccountV1Controller : Controller
{
    private readonly IIamAccountService _iamAccountService;

    public IamAccountV1Controller(IIamAccountService iamAccountService)
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

    [HttpPost]
    [Authorize(Policy = TinyCrmPermissions.Users.Create)]
    public async Task<ActionResult<UserDetailDto>> CreateAsync([FromBody] UserCreateDto userCreateDto)
    {
        var user = await _iamAccountService.CreateUserAsync(userCreateDto);
        return CreatedAtAction(nameof(GetDetailUser), new { id = user.Id }, user);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = TinyCrmPermissions.Users.Read)]
    public async Task<ActionResult<UserDetailDto>> GetDetailUser(Guid id)
    {
        var user = await _iamAccountService.GetDetailUserAsync(id);
        return Ok(user);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = TinyCrmPermissions.Users.Edit)]
    public async Task<ActionResult<UserEditDto>> UpdateUser(Guid id, [FromBody] UserEditDto userEditDto)
    {
        var user = await _iamAccountService.UpdateUserAsync(id, userEditDto);
        return Ok(user);
    }


    [HttpPost("{id:guid}/change-password")]
    [Authorize(Policy = TinyCrmPermissions.Users.Edit)]
    public async Task<ActionResult> ChangePassword(Guid id, [FromBody] UserChangePasswordDto userChangePasswordDto)
    {
        await _iamAccountService.ChangePasswordAsync(id, userChangePasswordDto);
        return NoContent();
    }

    [HttpDelete]
    [Authorize(Policy = TinyCrmPermissions.Users.Delete)]
    public async Task<ActionResult> DeleteManyUsers([FromBody] DeleteManyUsersDto deleteManyUsersDto)
    {
        await _iamAccountService.DeleteManyUsersAsync(deleteManyUsersDto);
        return NoContent();
    }
}