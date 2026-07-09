using Tasky.Application.Interfaces.Infrastructure;
using Tasky.Application.Interfaces.Services;
using Tasky.Domain.Entities;

namespace Tasky.Application.Services
{
    public class TaskService(ITaskRepository taskRepository) : ITaskService
    {
        public IEnumerable<TaskEntity> GetTasksByBoardId(Guid boardId)
        {     
            var tasks = taskRepository.GetTasksByBoardId(boardId).Result;
            return tasks;
        }
    }
}
