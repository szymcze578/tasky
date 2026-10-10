using Microsoft.AspNetCore.Mvc;

namespace Tasky.API.Controllers
{
    [ApiController]
    [Route("api/configuration")]
    public class ConfigurationController(IConfiguration configuration) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [Route("GetConfiguration")]
        public async Task<IActionResult> GetConfig()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var config = configuration["JWT:Issuer"];

            return Ok(userIdClaim);
        }
    }
}
