using ToDoList.Domain;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace ToDoList.Application.Dtos;

public record TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskType Type { get; set; }
    public string StateName { get; set; } = string.Empty;
}

public record CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskType Type { get; set; }
}

public record UpdateTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskType Type { get; set; }
}