using Tasky.API.ViewModels;
using Tasky.Application.Tasks.Dtos;

namespace Tasky.API.Mappers
{
    public static class TaskMapper
    {
        public static CreateTaskDTO Map(CreateTaskRequest request)
        {
            return new CreateTaskDTO
            {
                TaskName = request.TaskName,
                Status = request.Status,
            };
        }

    }
}
