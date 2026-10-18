using NSubstitute;
using Tasky.Application.Boards;
using Tasky.Application.Common.Abstractions;
using Tasky.Application.Users;
using Tasky.Domain.Entities;
using Tasky.Domain.Exceptions;

namespace Tasky.Application.Tests;

public class BoardsServiceTests
{
    private readonly IBoardsRepository _boards = Substitute.For<IBoardsRepository>();
    private readonly IUserRepository _users = Substitute.For<IUserRepository>();
    private readonly ICurrentUser _currentUser = Substitute.For<ICurrentUser>();
    private readonly BoardsService _sut;

    public BoardsServiceTests()
    {
        _currentUser.UserId.Returns(Guid.NewGuid());
        _sut = new BoardsService(_boards, _users, _currentUser);
    }

    [Fact]
    public async Task GetBoard_WhenBoardIsNotOwned_ThrowsNotFound()
    {
        _boards.GetOwnedById(Arg.Any<Guid>(), Arg.Any<Guid>())
               .Returns((BoardEntity?)null);

        await Assert.ThrowsAsync<BoardNotFoundException>(
            () => _sut.GetBoard(Guid.NewGuid()));
    }
}
