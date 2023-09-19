using BuildingBlock.Application.Identity.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAM.API.Controllers;

[ApiController]
[Route("api/iam-accounts")]
public class IamAccountController : Controller
{
    [HttpGet]
    [Authorize(Policy = TinyCrmPermissions.Users.Read)]
    public IActionResult GetAccounts()
    {
        return Ok("Day la list account");
    }
    
}