using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Models;

namespace ToDoList.Api.Data;

public class ToDoListContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }
    
    public ToDoListContext(DbContextOptions<ToDoListContext> options) 
        : base(options)
    {
    }
}