namespace ToDoListWeb.Models.States;

public class CompletedState : ITaskState
{
    public string Name => "Completed";

    public void Next(TaskItem task)
    {
        
    }
}