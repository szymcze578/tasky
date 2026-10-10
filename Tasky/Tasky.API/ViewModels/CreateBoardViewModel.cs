using Tasky.Domain.Enums;

namespace Tasky.API.ViewModels;

public class CreateBoardViewModel
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public BoardType Type { get; set; }
    public Guid OwnerId { get; set; }
}
