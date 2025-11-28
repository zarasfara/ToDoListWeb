using Microsoft.EntityFrameworkCore;
using ToDoList.Domain;
using ToDoList.Infrastructure.Configurations;

namespace ToDoList.Infrastructure;

public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}