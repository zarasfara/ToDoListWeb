namespace ToDoList.Domain.States;

public class CompletedState : ITaskState
{
    public string Name => "Completed";

    public void Next(TaskItem task)
    {
    }
}