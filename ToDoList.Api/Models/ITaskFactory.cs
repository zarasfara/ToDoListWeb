namespace ToDoList.Api.Models;

public interface ITaskFactory
{
    TaskItem CreateTask(TaskType type);
}