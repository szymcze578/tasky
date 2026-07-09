using Tasky.Domain.Entities;


namespace Tasky.Application.Interfaces.Infrastructure;

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetAll();
    Task<IEnumerable<TaskEntity>> GetTasksByBoardId(Guid boardId);
}