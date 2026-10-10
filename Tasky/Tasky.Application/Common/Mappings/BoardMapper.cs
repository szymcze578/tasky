using Tasky.Application.Boards.Dtos;
using Tasky.Domain.Entities;

namespace Tasky.Application.Common.Mappings;

public static class BoardMapper
{
    public static BoardDto Map(BoardEntity board)
    {
        return new BoardDto
        {
            Id = board.BoardId,
            OwnerId = board.OwnerId,
            Name = board.Name,
            Description = board.Description,
            Type = board.Type,
            TasksCompleted = board.Tasks.Count(task => task.Status == Status.Completed),
            TasksOpen = board.Tasks.Count(task => task.Status != Status.Completed && task.Status != Status.Canceled),
            CreationDate = board.CreationDate,
            UpdateDate = board.ModificationDate
        };
    }

}
