using Tasky.Domain.Entities;

namespace Tasky.Application.Interfaces.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskEntity> GetTasksByBoardId(Guid boardId);
    }
}
