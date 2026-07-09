using Tasky.API.Mappers;
using Tasky.API.ViewModels;
using Tasky.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CreateUser([FromBody] CreateUserViewModel user) {
            if(user == null) {
                return BadRequest("User data is empty!");
            }

            try
            {
                userService.CreateUser(UserMapper.Map(user));
            }
            catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            return Created();
        }
    }
}
