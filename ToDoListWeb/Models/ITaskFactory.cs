namespace ToDoListWeb.Models;

public interface ITaskFactory
{
    TaskItem CreateTask(TaskType type);
}