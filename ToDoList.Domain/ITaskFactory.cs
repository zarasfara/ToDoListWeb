namespace ToDoList.Domain;

public interface ITaskFactory
{
    TaskItem CreateTask(TaskType type);
}