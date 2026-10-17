using Tasky.Application.Boards.Dtos;

namespace Tasky.Application.Boards;

public interface IBoardsService
{
    Task<BoardDto> CreateBoard(CreateBoardRequest board);
    Task<BoardDto> UpdateBoard(Guid boardId, UpdateBoardRequest board);
    Task<IReadOnlyList<BoardDto>> GetBoards();
    Task<BoardDto> GetBoard(Guid boardId);
    Task DeleteBoard(Guid boardId);
}
