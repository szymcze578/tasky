using Tasky.Application.Model;

namespace Tasky.Application.Interfaces.Services
{
    public interface IBoardsService
    {
        Task<IEnumerable<BoardDTO>> GetBoardsForUser(Guid userId);

        Task<BoardDTO> AddBoard(CreateBoardDTO board);
    }
}
