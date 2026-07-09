using Tasky.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Application.Interfaces.Infrastructure
{
    public interface IBoardsRepository
    {
        Task<IEnumerable<BoardEntity>> GetBoardsForUser(Guid userId);

        Task<BoardEntity> AddBoard(BoardEntity board);
    }
}
