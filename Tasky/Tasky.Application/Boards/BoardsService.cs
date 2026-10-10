using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Application.Boards.Dtos;
using Tasky.Application.Common.Mappings;
using Tasky.Application.Users;
using Tasky.Domain.Entities;
using Tasky.Domain.Enums;

namespace Tasky.Application.Boards
{
    public class BoardsService(IBoardsRepository boardsRepository, IUserRepository userRepository) : IBoardsService
    {
        public async Task<IEnumerable<BoardDto>> GetBoardsForUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("User Id cannot be empty!");
            }

            var boards = await boardsRepository.GetBoardsForUser(userId);

            return boards.Select(BoardMapper.Map);
        }

        public async Task<BoardDto> AddBoard(Guid ownerId, CreateBoardRequest board)
        {
            if (string.IsNullOrWhiteSpace(board.Name))
            {
                throw new ArgumentException("Board name is required");
            }

            var ownerExists = await userRepository.ExistsById(ownerId);
            if (!ownerExists)
            {
                throw new KeyNotFoundException("Owner not found");
            }

            var boardEntity = BoardEntity.Create(ownerId, board.Name, board.Description, board.Type);

            await boardsRepository.AddBoard(boardEntity);
            return BoardMapper.Map(boardEntity);
        }
    }
}
