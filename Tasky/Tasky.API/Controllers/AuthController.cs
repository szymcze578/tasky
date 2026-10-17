using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Tasky.Application.Users;
using Tasky.Application.Users.Dtos;

namespace Tasky.API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<RegisterResponse>> Register(CreateUserRequest request, CancellationToken ct)
    {
        var userId = await authService.Register(request, ct);
        return StatusCode(StatusCodes.Status201Created, new RegisterResponse(userId));
    }

    [HttpPost("login")]
    [EnableRateLimiting("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Login(LoginUserRequest request, CancellationToken ct)
    {
        return Ok(await authService.Login(request, ct));
    }
}

public record RegisterResponse(Guid UserId);
