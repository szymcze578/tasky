using Tasky.Domain.Enums;

namespace Tasky.API.ViewModels
{
    public class BoardViewModel
    {
        public Guid Id { get; set; }
        public Guid ownerId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public BoardType Type {  get; set; }
        public DateTime creationDate { get; set; }
        public DateTime updateDate { get; set; } 
    }
}
