using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Tasky.Domain.Entities;

namespace Tasky.Infrastructure.Persistance.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<BoardEntity>
{
    public void Configure(EntityTypeBuilder<BoardEntity> builder)
    {
        builder.HasKey(board => board.BoardId);
        builder.Property(board => board.BoardId).ValueGeneratedNever();

        builder.Property(board => board.Name)
            .IsRequired();

        builder.HasOne(board => board.Owner)
            .WithMany()
            .HasForeignKey(board => board.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(board => board.Tasks)
            .WithOne()
            .HasForeignKey(task => task.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(board => board.Tasks)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
