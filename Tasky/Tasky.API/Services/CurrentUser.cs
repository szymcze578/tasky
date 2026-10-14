using Tasky.Application.Common.Abstractions;

namespace Tasky.API.Services;

public class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    public Guid UserId =>
        Guid.TryParse(accessor.HttpContext?.User.FindFirst("UserId")?.Value, out var id)
            ? id
            : throw new UnauthorizedAccessException("User id claim is missing.");
}
