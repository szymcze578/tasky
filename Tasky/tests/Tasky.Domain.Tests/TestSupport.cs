using Tasky.Domain.Entities;
using Tasky.Domain.Enums;

namespace Tasky.Domain.Tests;

internal static class TestData
{
    public static BoardEntity NewBoard(BoardType type = BoardType.Kanban)
        => BoardEntity.Create(Guid.NewGuid(), "Sprint 1", null, type);

    public static UserEntity NewUser()
        => UserEntity.Create("jan.kowalski", "jan@example.com", "hash");
}

internal static class TestClock
{
    public static void Tick()
    {
        var start = DateTime.UtcNow;
        SpinWait.SpinUntil(() => DateTime.UtcNow > start);
    }
}
