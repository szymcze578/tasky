using Tasky.Application.Boards.Dtos;

namespace Tasky.Application.Boards
{
    public interface IBoardsService
    {
        Task<IEnumerable<BoardDto>> GetBoardsForUser(Guid userId);

        Task<BoardDto> AddBoard(Guid userId, CreateBoardRequest board);
    }
}
