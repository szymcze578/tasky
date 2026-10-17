using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Tasky.Application.Boards;
using Tasky.Application.Tasks;
using Tasky.Application.Users;
using Tasky.Domain.Entities;


namespace Tasky.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBoardsService, BoardsService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
