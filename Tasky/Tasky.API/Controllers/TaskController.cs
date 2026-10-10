using Microsoft.AspNetCore.Mvc;
using Tasky.API.ViewModels;
using Tasky.API.Mappers;
using Tasky.Application.Tasks;

namespace Tasky.API.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController(ITaskService taskService) : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public IActionResult GetTasks(Guid boardId)
        {
            if (boardId == Guid.Empty)
            {
                return BadRequest("BoardId is required");
            }

            try { 
                var tasks = taskService.GetTasksByBoardId(boardId);
                return Ok(tasks);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddTask(Guid boardId, CreateTaskRequest createTask)
        {
            if (boardId == Guid.Empty)
            {
                return BadRequest("BoardId cannot be empty");
            }

            taskService.AddTask(boardId, TaskMapper.Map(createTask));

            return Ok("Task added");
        }
    }
}
