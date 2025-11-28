namespace ToDoList.Api.Models.States;

public class InProgressState : ITaskState
{
    public string Name => "InProgress";
    
    public void Next(TaskItem task)
    {
        task.State = new CompletedState();
        task.StateName = task.State.Name;
    }
}