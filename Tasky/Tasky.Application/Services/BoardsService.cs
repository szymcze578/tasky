using Tasky.Application.Interfaces.Infrastructure;
using Tasky.Application.Interfaces.Services;
using Tasky.Application.Mappers;
using Tasky.Application.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Domain.Enums;

namespace Tasky.Application.Services
{
    public class BoardsService(IBoardsRepository boardsRepository, IUserRepository userRepository) : IBoardsService
    {
        public async Task<IEnumerable<BoardDTO>> GetBoardsForUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("User Id cannot be empty!");
            }

            var boards = await boardsRepository.GetBoardsForUser(userId);

            return boards.Select(BoardMapper.Map);
        }

        public async Task<BoardDTO> AddBoard(CreateBoardDTO board)
        {
            if (string.IsNullOrWhiteSpace(board.Name))
            {
                throw new ArgumentException("Board name is required");
            }

            var ownerExists = await userRepository.ExistsById(board.OwnerId);
            if (!ownerExists)
            {
                throw new KeyNotFoundException("Owner not found");
            }

            var entity = BoardMapper.Map(board);
            var createdBoard = await boardsRepository.AddBoard(entity);

            return BoardMapper.Map(createdBoard);
        }
    }
}
