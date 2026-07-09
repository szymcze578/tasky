using Tasky.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasky.Infrastructure.Persistance
{
    public class BoardDbContext : DbContext
    {
        internal DbSet<UserEntity> Users => Set<UserEntity>();
        internal DbSet<BoardEntity> Boards => Set<BoardEntity>();
        internal DbSet<TaskEntity> BoardTasks => Set<TaskEntity>();

        public BoardDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(builder =>
            {
                builder.HasKey(user => user.UserId);
                builder.OwnsOne(user => user.Address);
            });

            modelBuilder.Entity<BoardEntity>(builder =>
            {
                builder.HasKey(board => board.BoardId);
            });

            modelBuilder.Entity<TaskEntity>(builder =>
            {
                builder.HasKey(task => task.TaskId);

                builder.HasOne<BoardEntity>()
                    .WithMany(board => board.Tasks)
                    .HasForeignKey(task => task.BoardId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(task => task.Responsible)
                    .WithMany(user => user.AssignedTasks)
                    .HasForeignKey(task => task.ResponsibleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
