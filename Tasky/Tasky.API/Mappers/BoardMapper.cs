using Tasky.API.ViewModels;
using Tasky.Application.Model;

namespace Tasky.API.Mappers
{
    public static class BoardMapper
    {
        public static BoardViewModel Map(BoardDTO dto)
        {
            return new BoardViewModel
            {
                Id = dto.Id,
                ownerId = dto.ownerId,
                Name = dto.Name,
                Description = dto.Description,
                Type = dto.Type,
                creationDate = dto.creationDate,
                updateDate = dto.updateDate
            };
        }

        public static CreateBoardDTO Map(CreateBoardViewModel viewModel)
        {
            return new CreateBoardDTO
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Type = viewModel.Type,
                OwnerId = viewModel.OwnerId
            };
        }
    }
}
