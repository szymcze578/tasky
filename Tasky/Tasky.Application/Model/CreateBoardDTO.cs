using Tasky.Domain.Enums;

namespace Tasky.Application.Model
{
    public class CreateBoardDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public BoardType Type { get; set; }
        public Guid OwnerId { get; set; }
    }
}
