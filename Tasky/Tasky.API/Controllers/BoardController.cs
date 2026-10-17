using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.Application.Boards;
using Tasky.Application.Boards.Dtos;

namespace Tasky.API.Controllers;


[Authorize]
[ApiController]
[Route("api/boards")]
public class BoardController(IBoardsService boardsService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BoardDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BoardDto>>> GetBoards()
    {
        return Ok(await boardsService.GetBoards());
    }

    [HttpGet("{boardId:guid}")]
    [ProducesResponseType(typeof(BoardDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BoardDto>> GetBoard(Guid boardId)
    {
        return Ok(await boardsService.GetBoard(boardId));
    }

    [HttpPost]
    [ProducesResponseType(typeof(BoardDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BoardDto>> CreateBoard(CreateBoardRequest request)
    {
        var board = await boardsService.CreateBoard(request);
        return CreatedAtAction(nameof(GetBoard), new { boardId = board.Id }, board);
    }

    [HttpPut("{boardId:guid}")]
    [ProducesResponseType(typeof(BoardDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BoardDto>> UpdateBoard(Guid boardId, UpdateBoardRequest request)
    {
        return Ok(await boardsService.UpdateBoard(boardId, request));
    }

    [HttpDelete("{boardId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBoard(Guid boardId)
    {
        await boardsService.DeleteBoard(boardId);
        return NoContent();
    }
}
