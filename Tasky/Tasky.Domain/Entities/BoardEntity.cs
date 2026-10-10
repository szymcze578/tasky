using Tasky.Domain.Enums;
using Tasky.Domain.Exceptions;

namespace Tasky.Domain.Entities;

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

    public static BoardEntity Create(Guid ownerId, string name, string? description, BoardType type)
    {
        if (ownerId == Guid.Empty)
            throw new InvalidBoardException("Board owner is required.");

        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidBoardException("Board name is required.");

        var now = DateTime.UtcNow;

        return new BoardEntity
        {
            BoardId = Guid.NewGuid(),
            OwnerId = ownerId,
            Name = name,
            Description = description,
            Type = type,
            CreationDate = now,
            ModificationDate = now
        };
    }
}
