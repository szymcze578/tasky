using Tasky.Application.Tasks.Dtos;
using Tasky.Domain.Entities;

namespace Tasky.Application.Tasks;

public interface ITaskService
{
    Task<IReadOnlyList<TaskDto>> GetTasks(Guid boardId);
    Task<TaskDto> GetTask(Guid boardId, Guid taskId);
    Task<TaskDto> AddTask(Guid boardId, CreateTaskRequest request);
    IEnumerable<TaskEntity> GetTasksByBoardId(Guid boardId);
    Task<TaskDto> ChangeStatus(Guid boardId, Guid taskId, Status status);
    Task<TaskDto> UpdateTask(Guid boardId, Guid taskId, UpdateTaskRequest request);
    Task DeleteTask(Guid boardId, Guid taskId);
}
