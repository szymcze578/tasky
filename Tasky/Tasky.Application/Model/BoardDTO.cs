using Tasky.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Application.Model
{
    public class BoardDTO
    {
        public Guid Id { get; set; }
        public Guid ownerId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public BoardType Type { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime updateDate { get; set; }
    }
}
