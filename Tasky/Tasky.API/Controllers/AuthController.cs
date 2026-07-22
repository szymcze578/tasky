using Microsoft.AspNetCore.Mvc;
using Tasky.API.Mappers;
using Tasky.API.ViewModels;
using Tasky.API.ViewModels.Auth;
using Tasky.Application.Interfaces.Services;
using Tasky.Application.Services;

namespace Tasky.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserViewModel user)
    {
        try
        {
            var userId = await authService.Register(UserMapper.Map(user));

            return CreatedAtAction(nameof(Register), userId);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUserRequest)
    {
        if (loginUserRequest == null 
            || string.IsNullOrEmpty(loginUserRequest.Login) 
            || string.IsNullOrEmpty(loginUserRequest.Password))
        {
            return BadRequest("Credentials are missing!");
        }

        try
        {
            return Ok(await authService.Login(UserMapper.Map(loginUserRequest)));
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error occured while loging in!");
        }
    }
}

