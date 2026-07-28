using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tasky.API.Mappers;
using Tasky.API.ViewModels.Auth;
using Tasky.Application.Exceptions;
using Tasky.Application.Interfaces.Services;
using Tasky.Application.Services;

namespace Tasky.API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [Route("register")]
    public async Task<IActionResult> Register(CreateUserRequest user)
    {
        try
        {
            var userId = await authService.Register(UserMapper.Map(user));

            return CreatedAtAction(nameof(Register), userId);
        }
        catch (DuplicateEmailException ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status409Conflict);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserRequest loginUserRequest)
    {
        if (loginUserRequest == null 
            || string.IsNullOrEmpty(loginUserRequest.Login) 
            || string.IsNullOrEmpty(loginUserRequest.Password))
        {
            return BadRequest("Credentials are missing!");
        }

        try
        {
            var result = await authService.Login(UserMapper.Map(loginUserRequest));

            return Ok(UserMapper.Map(result));
        }
        catch (InvalidCredentialsException)
        {
            return Unauthorized("Invalid login or password.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error occured while loging in!");
        }
    }
}

