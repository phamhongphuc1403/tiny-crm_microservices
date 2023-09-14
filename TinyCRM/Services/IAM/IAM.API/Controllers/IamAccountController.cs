using Microsoft.AspNetCore.Mvc;

namespace IAM.API.Controllers;

[ApiController]
[Route("api/iam-accounts")]
public class IamAccountController : Controller
{
    [HttpGet]
    public IActionResult GetAccounts()
    {
        return Ok("Day la list account");
    }
}