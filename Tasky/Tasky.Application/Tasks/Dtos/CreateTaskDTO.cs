using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Application.Tasks.Dtos
{
    public class CreateTaskDTO
    {
        public string TaskName { get; set; }
        public Status Status { get; set; }
    }
}
