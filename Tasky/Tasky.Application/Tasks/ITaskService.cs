using Tasky.Application.Tasks.Dtos;
using Tasky.Domain.Entities;

namespace Tasky.Application.Tasks;

public interface ITaskService
{
    IEnumerable<TaskEntity> GetTasksByBoardId(Guid boardId);
    void AddTask(Guid boardId, CreateTaskDTO task);
}
