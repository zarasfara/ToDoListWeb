using ToDoList.Application.Dtos;
using ToDoList.Domain;
using ToDoList.Domain.Repositories;
using TaskFactory = ToDoList.Domain.TaskFactory;

namespace ToDoList.Application.Services;

public interface ITaskService
{
    Task<List<TaskDto>> GetTasksAsync(string? state, TaskType? type);

    Task<TaskDto?> GetTaskByIdAsync(int id);

    Task<TaskDto> CreateTaskAsync(CreateTaskDto createDto);

    Task UpdateTaskAsync(UpdateTaskDto updateDto);

    Task DeleteTaskAsync(int id);

    Task MoveToNextStateAsync(int id);
}

public sealed class TaskService(ITaskRepository taskRepository) : ITaskService
{
    private readonly TaskFactory _taskFactory = new();

    public async Task<List<TaskDto>> GetTasksAsync(string? state, TaskType? type)
    {
        var tasks = await taskRepository.GetAllAsync(state, type);
        return tasks.Select(MapToDto).ToList();
    }

    public async Task<TaskDto?> GetTaskByIdAsync(int id)
    {
        var task = await taskRepository.GetByIdAsync(id);
        return task == null ? null : MapToDto(task);
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createDto)
    {
        var task = _taskFactory.CreateTask(createDto.Type);
        task.Title = createDto.Title;
        task.Description = createDto.Description;

        await taskRepository.AddAsync(task);
        await taskRepository.SaveChangesAsync();

        return MapToDto(task);
    }

    public async Task UpdateTaskAsync(UpdateTaskDto updateDto)
    {
        var task = await taskRepository.GetByIdAsync(updateDto.Id);
        if (task == null)
        {
            throw new ArgumentException($"Task with id {updateDto.Id} not found");
        }

        task.Title = updateDto.Title;
        task.Description = updateDto.Description;
        task.Type = updateDto.Type;

        taskRepository.Update(task);
        await taskRepository.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await taskRepository.GetByIdAsync(id);
        if (task != null)
        {
            taskRepository.Delete(task);
            await taskRepository.SaveChangesAsync();
        }
    }

    public async Task MoveToNextStateAsync(int id)
    {
        var task = await taskRepository.GetByIdAsync(id);
        if (task == null)
        {
            throw new ArgumentException($"Task with id {id} not found");
        }

        task.SyncStateObject();
        task.MoveToNextState();

        taskRepository.Update(task);
        await taskRepository.SaveChangesAsync();
    }

    private static TaskDto MapToDto(TaskItem task)
        => new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Type = task.Type,
            StateName = task.StateName ?? "Unknown",
        };
}