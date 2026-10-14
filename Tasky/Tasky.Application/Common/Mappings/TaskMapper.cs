using Tasky.Application.Tasks.Dtos;
using Tasky.Domain.Entities;

namespace Tasky.Application.Common.Mappings;

public static class TaskMapper
{
    public static TaskDto Map(TaskEntity task) => new()
    {
        TaskId = task.TaskId,
        BoardId = task.BoardId,
        Title = task.Title,
        Description = task.Description,
        ResponsibleId = task.ResponsibleId,
        Status = task.Status,
        Estimate = task.Estimate,
        Position = task.Position,
        DueDate = task.DueDate,
        CompletedDate = task.CompletedDate,
        CreationDate = task.CreationDate,
        ModificationDate = task.ModificationDate
    };

}
