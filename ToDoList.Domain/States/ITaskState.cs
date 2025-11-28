namespace ToDoList.Domain.States;

public interface ITaskState
{
    string Name { get; }

    void Next(TaskItem item);
}