using Tasky.Domain.Enums;

namespace Tasky.Domain.Entities
{
    public class BoardEntity
    {
        public Guid BoardId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public BoardType Type { get; set; }

        public Guid OwnerId { get; set; }
        public UserEntity Owner { get; set; } = null!;

        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
