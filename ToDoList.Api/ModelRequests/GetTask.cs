using ToDoList.Domain;

namespace ToDoList.Api.ModelRequests;

public class GetTask
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public TaskType Type { get; init; }
    public string StateName { get; init; } = string.Empty;
}