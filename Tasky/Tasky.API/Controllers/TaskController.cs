using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.API.Mappers;
using Tasky.API.ViewModels;
using Tasky.Application.Tasks;
using Tasky.Application.Tasks.Dtos;

namespace Tasky.API.Controllers;

[Authorize]
[ApiController]
[Route("api/boards/{boardId:guid}/tasks")]
public class TaskController(ITaskService taskService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskDto>>> GetTasks(Guid boardId)
    {
        return Ok(await taskService.GetTasks(boardId));
    }

    [HttpGet("{taskId:guid}")]
    public async Task<ActionResult<TaskDto>> GetTask(Guid boardId, Guid taskId)
    {
        return Ok(await taskService.GetTask(boardId, taskId));
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> AddTask(
        Guid boardId, CreateTaskRequest request)
    {
        var task = await taskService.AddTask(boardId, request);
        return CreatedAtAction(nameof(GetTask), new { boardId, taskId = task.TaskId }, task);
    }

    [HttpPut("{taskId:guid}")]
    public async Task<ActionResult<TaskDto>> UpdateTask(
    Guid boardId, Guid taskId, UpdateTaskRequest request)
    {
        return Ok(await taskService.UpdateTask(boardId, taskId, request));
    }

    [HttpPut("{taskId:guid}/status")]
    public async Task<ActionResult<TaskDto>> ChangeStatus(
        Guid boardId, Guid taskId, ChangeTaskStatusRequest request)
    {
        return Ok(await taskService.ChangeStatus(boardId, taskId, request.Status));
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> DeleteTask(Guid boardId, Guid taskId)
    {
        await taskService.DeleteTask(boardId, taskId);
        return NoContent();
    }
}
