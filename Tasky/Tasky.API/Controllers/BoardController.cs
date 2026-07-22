using Tasky.API.Mappers;
using Tasky.API.ViewModels;
using Tasky.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Tasky.Application.Interfaces.Infrastructure;

namespace Tasky.API.Controllers
{

    [Route("api/board")]
    [ApiController]
    public class BoardController(IBoardsService boardsService, IConfiguration configuration) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [Route("GetConfiguration")]
        public async Task<IActionResult> GetConfig()
        {
            var config = configuration["JWT:Issuer"];

            return Ok(config);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BoardViewModel>), 200)]
        [Route("")]
        public async Task<IActionResult> GetBoardsForUser(Guid userId)
        {
            if( userId == Guid.Empty)
            {
                return BadRequest("UserId is required");
            }

            try
            {
                var boards = await boardsService.GetBoardsForUser(userId);

                return Ok(boards.Select(BoardMapper.Map));
            }
            catch
            {
                return BadRequest("Failed to fetch boards!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBoard([FromBody] CreateBoardViewModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Board name is required");
            }

            if (request.OwnerId == Guid.Empty)
            {
                return BadRequest("OwnerId is required");
            }

            if (!Enum.IsDefined(request.Type))
            {
                return BadRequest("Invalid board type");
            }

            try
            {
                var createdBoard = await boardsService.AddBoard(BoardMapper.Map(request));

                return Ok(BoardMapper.Map(createdBoard));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Owner not found");
            }
        }
    }
}
