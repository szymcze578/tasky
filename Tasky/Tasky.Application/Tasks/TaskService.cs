using Tasky.Application.Tasks.Dtos;
using Tasky.Domain.Entities;

namespace Tasky.Application.Tasks;

public class TaskService(ITaskRepository taskRepository) : ITaskService
{
    public IEnumerable<TaskEntity> GetTasksByBoardId(Guid boardId)
    {     
        var tasks = taskRepository.GetTasksByBoardId(boardId).Result;
        return tasks;
    }

    public void AddTask(Guid boardId, CreateTaskDTO task)
    {

        return;
    }
}
