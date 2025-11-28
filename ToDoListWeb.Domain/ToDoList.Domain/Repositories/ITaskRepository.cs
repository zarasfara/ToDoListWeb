namespace ToDoList.Domain.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(int id);

    Task<List<TaskItem>> GetAllAsync(string? state = null, TaskType? type = null);

    Task AddAsync(TaskItem task);

    void Update(TaskItem task);

    void Delete(TaskItem task);

    Task SaveChangesAsync();
}