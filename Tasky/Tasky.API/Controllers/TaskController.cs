using Tasky.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Tasky.API.Controllers
{
    [Route("api/task")]
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
        public IActionResult AddTask(Guid boardId )
        {
            return Ok("Task added");
        }
    }
}
