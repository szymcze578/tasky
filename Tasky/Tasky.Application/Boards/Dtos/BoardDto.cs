using Tasky.Domain.Enums;

namespace Tasky.Application.Boards.Dtos;

public class BoardDto
{
    public required Guid Id { get; set; }
    public required Guid OwnerId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int TasksCompleted {  get; set; }
    public int TasksOpen { get; set; }
    public required BoardType Type { get; set; }
    public required DateTime CreationDate { get; set; }
    public required DateTime UpdateDate { get; set; }
}