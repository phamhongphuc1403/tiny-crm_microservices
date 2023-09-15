using IAM.Business.Models;
using IAM.Business.Models.Dto.Users;
using IAM.Business.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAM.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync(SignInDto signInDto)
    {
        var result = await _authService.SignInAsync(signInDto);
        return Ok(new { Token = result });
    }

    [HttpGet]
    [Authorize]
    public IActionResult AuthenticateAsync()
    {
        var claims = User.Claims.Select(x => new AuthenticationClaim(x.Type, x.Value));
        var user = User.Identity?.Name;
        return Ok(claims);
    }
}