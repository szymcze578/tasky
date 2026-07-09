using Tasky.Application.Model;
using Tasky.Domain.Entities;

namespace Tasky.Application.Mappers
{
    public static class BoardMapper
    {
        public static BoardDTO Map(BoardEntity board)
        {
            return new BoardDTO
            {
                Id = board.BoardId,
                ownerId = board.OwnerId,
                Name = board.Name,
                Description = board.Description,
                Type = board.Type,
                creationDate = board.CreationDate,
                updateDate = board.ModificationDate
            };
        }

        public static BoardEntity Map(CreateBoardDTO dto)
        {
            var now = DateTime.UtcNow;

            return new BoardEntity
            {
                BoardId = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Type = dto.Type,
                OwnerId = dto.OwnerId,
                CreationDate = now,
                ModificationDate = now
            };
        }
    }
}
