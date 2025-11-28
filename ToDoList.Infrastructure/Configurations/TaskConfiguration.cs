using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain;

namespace ToDoList.Infrastructure.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
            
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.Description)
            .HasMaxLength(1000);
            
        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<int>();
            
        builder.Property(x => x.StateName)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.ToTable("Tasks");
    }
}