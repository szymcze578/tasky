
namespace Tasky.Domain.Entities
{
    public class TaskEntity
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid BoardId { get; set; }

        public Guid ResponsibleId { get; set; }
        public UserEntity Responsible { get; set; }
        
        public Status Status { get; set; }
        public int Estimate { get; set; }

        public int Position { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
