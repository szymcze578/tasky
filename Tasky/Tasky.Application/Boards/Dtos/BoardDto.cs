using Tasky.Domain.Enums;

namespace Tasky.Application.Boards.Dtos;

public class BoardDto
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int TasksCompleted {  get; set; }
    public int TasksOpen { get; set; }
    public BoardType Type { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
}