using Tasky.Application.Interfaces.Infrastructure;
using Tasky.Domain.Entities;
using Tasky.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Tasky.Infrastructure.Implementation;

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
            .Where(board => board.OwnerId == userId)
            .ToListAsync();
    }

    public async Task<BoardEntity> AddBoard(BoardEntity board)
    {
        await context.Boards.AddAsync(board);
        await context.SaveChangesAsync();

        return board;
    }
}
