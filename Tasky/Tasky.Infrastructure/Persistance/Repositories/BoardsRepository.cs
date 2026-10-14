using Tasky.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Tasky.Application.Boards;

namespace Tasky.Infrastructure.Persistance.Repositories;

public class BoardsRepository : IBoardsRepository
{
    private readonly BoardDbContext context;

    public BoardsRepository(BoardDbContext dbContext)
    {
        context = dbContext;
    }

    public async Task<IEnumerable<BoardEntity>> GetBoardsForUser(Guid userId)
    {
        return await context.Boards
            .AsNoTracking()
            .Include(board => board.Tasks)
            .Where(board => board.OwnerId == userId)
            .ToListAsync();
    }

    public async Task AddBoard(BoardEntity board)
    {
        await context.Boards.AddAsync(board);
        await context.SaveChangesAsync();
    }

    public async Task<BoardEntity?> GetOwnedById(Guid boardId, Guid ownerId)
    => await context.Boards
        .Include(b => b.Tasks)
        .FirstOrDefaultAsync(b => b.BoardId == boardId && b.OwnerId == ownerId);


    public Task SaveChanges() => context.SaveChangesAsync();
}
