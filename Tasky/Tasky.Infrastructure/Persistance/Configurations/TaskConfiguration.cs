using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasky.Domain.Entities;

namespace Tasky.Infrastructure.Persistance.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.HasKey(task => task.TaskId);
        builder.Property(task => task.TaskId).ValueGeneratedNever();

        builder.Property(task => task.Title)
            .IsRequired();

        builder.Property(task => task.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(task => task.Responsible)
            .WithMany(user => user.AssignedTasks)
            .HasForeignKey(task => task.ResponsibleId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
