using BuildingBlock.Application.Identity.Permissions;
using IAM.Business.Models.Users.Dto;
using IAM.Business.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAM.API.Controllers;

[ApiController]
[Route("api/v2/iam-accounts/")]
public class IamAccountV2Controller : Controller
{
    private readonly IIamAccountService _iamAccountService;

    public IamAccountV2Controller(IIamAccountService iamAccountService)
    {
        _iamAccountService = iamAccountService;
    }

    [HttpGet]
    [Authorize(Policy = TinyCrmPermissions.Users.Read)]
    public async Task<ActionResult<IEnumerable<UserSummaryDto>>> FilteredAsync(
        [FromQuery] FilterUsersDto dto)
    {
        var users = await _iamAccountService.FilterUsersAsync(dto);
        return Ok(users);
    }
}