using Tasky.API.Mappers;
using Tasky.API.ViewModels;
using Tasky.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tasky.API.ViewModels.Auth;

namespace Tasky.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public IActionResult GetUsers()
        {
            return Ok("User endpoint is working");
        }


        [HttpPost]
        [Route("{userId}/address")]
        public async Task<IActionResult> SetAddress(Guid userId, [FromBody] AddressViewModel address)
        {
            await userService.SetAddress(userId, AddressMapper.Map(address));
            return NoContent();
        }
    }
}
