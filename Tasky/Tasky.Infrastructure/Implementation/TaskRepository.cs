using Tasky.Application.Interfaces.Infrastructure;
using Tasky.Domain.Entities;
using Tasky.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Tasky.Infrastructure.Implementation;

public class TaskRepository : ITaskRepository
{

    private readonly BoardDbContext context;

    public TaskRepository(BoardDbContext dbContext)
    {
        context = dbContext;
    }

    public async Task<List<TaskEntity>> GetAll()
    {
        return await context.BoardTasks.Include(task => task.Responsible).ToListAsync();
    }

    public async Task<IEnumerable<TaskEntity>> GetTasksByBoardId(Guid boardId)
    {
        return await context.BoardTasks
            .Where(task => task.BoardId == boardId)
            .Include(task => task.Responsible)
            .ToListAsync();
    }
}
