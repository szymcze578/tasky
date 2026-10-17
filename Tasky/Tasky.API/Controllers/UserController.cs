using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.Application.Users;
using Tasky.Application.Users.Dtos;

namespace Tasky.API.Controllers;

[Authorize]
[ApiController]
[Route("api/users/me")]
public class UserController(IUserService userService) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> GetMe(CancellationToken ct)
        => Ok(await userService.GetMe(ct));

    [HttpPut]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> UpdateMe(UpdateUserRequest request, CancellationToken ct)
        => Ok(await userService.UpdateMe(request, ct));

    [HttpPut("address")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> SetAddress(AddressRequest request, CancellationToken ct)
        => Ok(await userService.SetAddress(request, ct));

    [HttpDelete("address")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveAddress(CancellationToken ct)
    {
        await userService.RemoveAddress(ct);
        return NoContent();
    }
}
