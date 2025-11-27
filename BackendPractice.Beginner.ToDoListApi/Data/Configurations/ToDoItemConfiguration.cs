using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendPractice.Beginner.TodoListAPI.Models;

namespace BackendPractice.Beginner.TodoListAPI.Data.Configurations;

public class ToDoItemConfiguration:IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.Title)
            .IsRequired();

        builder.Property(i => i.Description)
            .IsRequired();

        builder.HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId);
    }
}