namespace ToDoListWeb.Models;

public class TaskFactory : ITaskFactory
{
    public TaskItem CreateTask(TaskType type)
    {
        return new TaskItem
        {
            Type = type,
            StateName = "New"
        };
    }
}