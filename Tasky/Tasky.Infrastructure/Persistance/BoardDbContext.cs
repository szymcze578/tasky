using Tasky.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasky.Infrastructure.Persistance;

public class BoardDbContext : DbContext
{
    internal DbSet<UserEntity> Users => Set<UserEntity>();
    internal DbSet<BoardEntity> Boards => Set<BoardEntity>();
    internal DbSet<TaskEntity> BoardTasks => Set<TaskEntity>();

    public BoardDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoardDbContext).Assembly);
    }
}
