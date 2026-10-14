using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.Application.Boards;
using Tasky.Application.Boards.Dtos;

namespace Tasky.API.Controllers;


[Authorize]
[ApiController]
[Route("api/boards")]
public class BoardController(IBoardsService boardsService, IConfiguration configuration) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BoardDto>), 200)]
    [Route("")]
    public async Task<IActionResult> GetBoardsForUser()
    {
        var userId = Guid.Parse(User.FindFirst("UserId")?.Value);

        if ( userId == Guid.Empty)
        {
            return BadRequest("UserId is required");
        }

        try
        {
            var boards = await boardsService.GetBoardsForUser(userId);

            return Ok(boards);
        }
        catch
        {
            return BadRequest("Failed to fetch boards!");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(BoardDto), 200)]
    [Route("")]
    public async Task<IActionResult> AddBoard([FromBody] CreateBoardRequest request)
    {
        var userId = Guid.Parse(User.FindFirst("UserId")?.Value);

        if (userId == Guid.Empty)
        {
            return BadRequest("UserId is required");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Board name is required");
        }

        if (!Enum.IsDefined(request.Type))
        {
            return BadRequest("Invalid board type");
        }

        try
        {
            var createdBoard = await boardsService.AddBoard(userId, request);

            return Ok(createdBoard);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Owner not found");
        }
    }
}
