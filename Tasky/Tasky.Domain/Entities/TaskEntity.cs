
using Tasky.Domain.Exceptions;

namespace Tasky.Domain.Entities
{
    public class TaskEntity
    {
        public Guid TaskId { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }

        public Guid BoardId { get; private set; }

        public Guid? ResponsibleId { get; private set; }
        public UserEntity? Responsible { get; private set; }
        
        public Status Status { get; private set; }
        public int? Estimate { get; private set; }

        public int Position { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime? CompletedDate { get; private set; }

        public DateTime CreationDate { get; private set; }
        public DateTime ModificationDate { get; private set; }

        private TaskEntity() { }

        internal static TaskEntity Create(
            Guid boardId, 
            string title, 
            string? description, 
            Guid? responsibleId, 
            int? estimate, 
            DateTime? dueDate, 
            int position)
        {
            if (estimate < 0)
                throw new InvalidTaskException("Estimate cannot be negative.");

            var now = DateTime.UtcNow;

            return new TaskEntity
            {
                TaskId = Guid.NewGuid(),
                BoardId = boardId,
                Title = ValidateTitle(title),
                Description = description,
                ResponsibleId = responsibleId,
                Status = Status.ToDo,
                Estimate = estimate,
                DueDate = dueDate,
                Position = position,
                CreationDate = now,
                ModificationDate = now
            };
        }

        internal void ChangeStatus(Status status)
        {
            if (Status == status) return;

            Status = status;
            CompletedDate = status == Status.Completed ? DateTime.UtcNow : null;
            UpdateModificationDate();
        }

        public void Update(string title, string? description, int? estimate, DateTime? dueDate)
        {
            Title = ValidateTitle(title);
            Description = description;
            Estimate = ValidateEstimate(estimate);
            DueDate = dueDate;
            UpdateModificationDate();
        }

        private static string ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidTaskException("Task title is required.");

            title = title.Trim();

            return title;
        }

        private static int? ValidateEstimate(int? estimate)
        {
            if (estimate < 0)
                throw new InvalidTaskException("Estimate cannot be negative.");
            return estimate;
        }

        private void UpdateModificationDate() => ModificationDate = DateTime.UtcNow;
    }
}
