using Tasky.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Application.Boards;

public interface IBoardsRepository
{
    Task<IEnumerable<BoardEntity>> GetBoardsForUser(Guid userId);
    Task<BoardEntity?> GetOwnedById(Guid boardId, Guid ownerId);
    Task AddBoard(BoardEntity board);
    Task SaveChanges();
}
