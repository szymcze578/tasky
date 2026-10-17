using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Application.Boards.Dtos;
using Tasky.Application.Common.Abstractions;
using Tasky.Application.Common.Mappings;
using Tasky.Application.Users;
using Tasky.Domain.Entities;
using Tasky.Domain.Enums;
using Tasky.Domain.Exceptions;

namespace Tasky.Application.Boards;

public class BoardsService(
    IBoardsRepository boards, 
    IUserRepository users, 
    ICurrentUser currentUser) : IBoardsService
{
    public async Task<IReadOnlyList<BoardDto>> GetBoards()
    {
        var entities = await boards.GetByOwnerId(currentUser.UserId);
        return entities.Select(b => BoardMapper.Map(b)).ToList();
    }

    public async Task<BoardDto> GetBoard(Guid boardId)
    {
        var board = await GetOwnedBoard(boardId);
        return BoardMapper.Map(board);
    }

    public async Task<BoardDto> CreateBoard(CreateBoardRequest request)
    {
        var ownerId = currentUser.UserId;

        if (!await users.ExistsById(ownerId))
            throw new UserNotFoundException(ownerId);

        var board = BoardEntity.Create(ownerId, request.Name, request.Description, request.Type);

        await boards.Add(board);
        await boards.SaveChanges();

        return BoardMapper.Map(board);
    }

    public async Task<BoardDto> UpdateBoard(Guid boardId, UpdateBoardRequest request)
    {
        var board = await GetOwnedBoard(boardId);

        board.Update(request.Name, request.Description);

        await boards.SaveChanges();
        return BoardMapper.Map(board);
    }

    public async Task DeleteBoard(Guid boardId)
    {
        var board = await GetOwnedBoard(boardId);

        boards.Remove(board);

        await boards.SaveChanges();
    }

    private async Task<BoardEntity> GetOwnedBoard(Guid boardId)
        => await boards.GetOwnedById(boardId, currentUser.UserId)
           ?? throw new BoardNotFoundException(boardId);
}
