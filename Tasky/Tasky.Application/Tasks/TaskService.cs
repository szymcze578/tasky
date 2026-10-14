using Tasky.Application.Boards;
using Tasky.Application.Common.Abstractions;
using Tasky.Application.Common.Mappings;
using Tasky.Application.Tasks.Dtos;
using Tasky.Application.Users;
using Tasky.Domain.Entities;
using Tasky.Domain.Exceptions;

namespace Tasky.Application.Tasks;

public class TaskService(
    ITaskRepository taskRepository, 
    IBoardsRepository boards,
    IUserRepository users,
    ICurrentUser currentUser) : ITaskService
{

    public async Task<IReadOnlyList<TaskDto>> GetTasks(Guid boardId)
    {
        var board = await GetOwnedBoard(boardId);
        return board.Tasks.OrderBy(t => t.Position).Select(t => TaskMapper.Map(t)).ToList();
    }

    public async Task<TaskDto> GetTask(Guid boardId, Guid taskId)
    {
        var task = (await GetOwnedBoard(boardId)).GetTask(taskId);
        return TaskMapper.Map(task);
    }

    public IEnumerable<TaskEntity> GetTasksByBoardId(Guid boardId)
    {     
        var tasks = taskRepository.GetTasksByBoardId(boardId).Result;
        return tasks;
    }

    public async Task<TaskDto> AddTask(Guid boardId, CreateTaskRequest request)
    {
        var board = await GetOwnedBoard(boardId);
        await EnsureUserExists(request.ResponsibleId);

        var task = board.AddTask(
            request.Title, request.Description, request.ResponsibleId, request.Estimate, request.DueDate);

        await boards.SaveChanges();
        return TaskMapper.Map(task);
    }

    public async Task<TaskDto> ChangeStatus(Guid boardId, Guid taskId, Status status)
    {
        var board = await GetOwnedBoard(boardId);
        
        var task = board.ChangeTaskStatus(taskId, status);

        await boards.SaveChanges();
        return TaskMapper.Map(task);
    }

    public async Task<TaskDto> UpdateTask(Guid boardId, Guid taskId, UpdateTaskRequest request)
    {
        var board = await GetOwnedBoard(boardId);
        var task = board.GetTask(taskId);

        task.Update(request.Title, request.Description, request.Estimate, request.DueDate);

        await boards.SaveChanges();
        return TaskMapper.Map(task);
    }

    public async Task DeleteTask(Guid boardId, Guid taskId)
    {
        var board = await GetOwnedBoard(boardId);
        board.RemoveTask(taskId);
        await boards.SaveChanges();
    }

    private async Task<BoardEntity> GetOwnedBoard(Guid boardId)
    => await boards.GetOwnedById(boardId, currentUser.UserId)
       ?? throw new BoardNotFoundException(boardId);

    private async Task EnsureUserExists(Guid? userId)
    {
        if (userId is { } id && !await users.ExistsById(id))
            throw new UserNotFoundException(id);
    }
}
