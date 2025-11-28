using Microsoft.EntityFrameworkCore;
using ToDoList.Domain;
using ToDoList.Domain.Repositories;

namespace ToDoList.Infrastructure.Repositories;

public class TaskRepository(DatabaseContext context) : ITaskRepository
{
    public async Task<TaskItem?> GetByIdAsync(int id)
        => await context.Tasks.FindAsync(id);

    public async Task<List<TaskItem>> GetAllAsync(string? state = null, TaskType? type = null)
    {
        var query = context.Tasks.AsQueryable();

        if (!string.IsNullOrEmpty(state))
        {
            query = query.Where(t => t.StateName == state);
        }

        if (type != null)
        {
            query = query.Where(t => t.Type == type);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(TaskItem task)
    {
        await context.Tasks.AddAsync(task);
    }

    public void Update(TaskItem task)
    {
        context.Tasks.Update(task);
    }

    public void Delete(TaskItem task)
    {
        context.Tasks.Remove(task);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}