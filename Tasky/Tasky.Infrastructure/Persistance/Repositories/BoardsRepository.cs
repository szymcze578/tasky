using Tasky.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Tasky.Application.Boards;

namespace Tasky.Infrastructure.Persistance.Repositories;

public class BoardsRepository(BoardDbContext context) : IBoardsRepository
{
    public async Task<IReadOnlyList<BoardEntity>> GetByOwnerId(Guid ownerId)
    {
        return await context.Boards
            .AsNoTracking()
            .Include(b => b.Tasks)
            .Where(b => b.OwnerId == ownerId)
            .OrderBy(b => b.CreationDate)
            .ToListAsync();
    }

    public async Task<BoardEntity?> GetOwnedById(Guid boardId, Guid ownerId)
    {
        return await context.Boards
            .Include(b => b.Tasks)
            .FirstOrDefaultAsync(b => b.BoardId == boardId && b.OwnerId == ownerId);
    }

    public async Task Add(BoardEntity board)
    {
        await context.Boards.AddAsync(board);
    }

    public void Remove(BoardEntity board)
    {
        context.Boards.Remove(board);
    }

    public Task SaveChanges() => context.SaveChangesAsync();
}
