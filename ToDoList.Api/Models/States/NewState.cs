namespace ToDoList.Api.Models.States;

public class NewState : ITaskState
{
    public string Name => "New";
    
    public void Next(TaskItem task)
    {
        task.State = new InProgressState();
        task.StateName = task.State.Name;
    }
}