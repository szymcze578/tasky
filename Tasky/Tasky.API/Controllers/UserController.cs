using Tasky.API.Mappers;
using Tasky.API.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tasky.API.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Tasky.Application.Users;

namespace Tasky.API.Controllers;


[ApiController]
[Authorize]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("me")]
    public async Task<IActionResult> GetUser()
    {
        var userIdClaim = Guid.Parse(User.FindFirst("UserId")?.Value);

        if (userIdClaim == null) {
            return Unauthorized();
        }

        var user = await userService.GetUser(userIdClaim);
        return Ok(UserMapper.Map(user));
    }


    [HttpPost]
    [Route("{userId}/address")]
    public async Task<IActionResult> SetAddress(Guid userId, [FromBody] AddressViewModel address)
    {
        await userService.SetAddress(userId, AddressMapper.Map(address));
        return NoContent();
    }
}
