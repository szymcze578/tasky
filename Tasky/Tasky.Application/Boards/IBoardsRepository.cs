using Tasky.Domain.Entities;

namespace Tasky.Application.Boards;

public interface IBoardsRepository
{
    Task<IReadOnlyList<BoardEntity>> GetByOwnerId(Guid ownerId);
    Task<BoardEntity?> GetOwnedById(Guid boardId, Guid ownerId);
    Task Add(BoardEntity board);
    void Remove(BoardEntity board);
    Task SaveChanges();
}
