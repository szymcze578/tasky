using System.ComponentModel.DataAnnotations;

namespace Tasky.API.ViewModels
{
    public class CreateTaskRequest
    {
        [Required(ErrorMessage = "Task name is required")]
        public string TaskName { get; set; }

        public Status Status { get; set; }
    }
}
