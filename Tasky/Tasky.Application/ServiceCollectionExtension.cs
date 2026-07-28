using Tasky.Application.Interfaces.Services;
using Tasky.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Tasky.Domain.Entities;


namespace Tasky.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBoardsService, BoardsService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

            return services;
        }
    }
}
