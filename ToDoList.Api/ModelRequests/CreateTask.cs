using System.ComponentModel.DataAnnotations;
using ToDoList.Domain;

namespace ToDoList.Api.ModelRequests;

public class CreateTask
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
    public string Title { get; init; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
    public string Description { get; init; } = string.Empty;

    [Required(ErrorMessage = "Type is required")]
    public TaskType Type { get; init; } = TaskType.Regular;
}