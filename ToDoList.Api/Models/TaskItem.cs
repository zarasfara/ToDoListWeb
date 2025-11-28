using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.Api.Models.States;

namespace ToDoList.Api.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public TaskType Type { get; set; }
    public string StateName { get; set; } = "New";
    [NotMapped]
    public ITaskState State { get; set; } = new NewState();

    public void SyncStateObject()
    {
        State = StateName switch
        {
            "New" => new NewState(),
            "InProgress" => new InProgressState(),
            "Completed" => new CompletedState(),
            _ => new NewState()
        };
    }

    public void MoveToNextState()
    {
        State.Next(this);
    }
}