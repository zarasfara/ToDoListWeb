using Microsoft.EntityFrameworkCore;
using ToDoListWeb.Models;

namespace ToDoListWeb.Data;

public class ToDoListContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }
    
    public ToDoListContext(DbContextOptions<ToDoListContext> options) 
        : base(options)
    {
    }
}