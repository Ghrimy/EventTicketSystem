using EventTicketSystem_DTOs.AuthDto;
using EventTicketSystem.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
    {
        try
        {
            return Ok(await authService.RegisterUserAsync(registerUserDto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        try
        {
            return Ok(await authService.LoginUserAsync(loginUserDto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("me")]
    public IActionResult GetMe()
    {
       return Ok(authService.GetCurrentUser());
    }

}