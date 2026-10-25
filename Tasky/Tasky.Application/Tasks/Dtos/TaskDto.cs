namespace Tasky.Application.Tasks.Dtos;

public class TaskDto
{
    public required Guid TaskId { get; set; }
    public required Guid BoardId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    public Guid? ResponsibleId { get; set; }

    public required Status Status { get; set; }
    public int? Estimate { get; set; }

    public int Position { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }

    public required DateTime CreationDate { get; set; }
    public required DateTime ModificationDate { get; set; }
}
