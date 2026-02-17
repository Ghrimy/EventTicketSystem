using System.ComponentModel.DataAnnotations;
using EventTicketSystem_DTOs.AuthDto;
using EventTicketSystem.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthController(IAuthService authService) : ControllerBase
{
    
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
    {
        return Ok(await authService.RegisterUserAsync(registerUserDto));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        return Ok(await authService.LoginUserAsync(loginUserDto));
    }
    
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("me")]
    public IActionResult GetMe()
    {
       return Ok(authService.GetCurrentUser());
    }

}