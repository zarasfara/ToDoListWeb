using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.ModelRequests;
using ToDoList.Application.Dtos;
using ToDoList.Application.Services;
using ToDoList.Domain;

namespace ToDoList.Api.Controllers;

public class TasksController(ITaskService taskApiService) : Controller
{
    /// <summary>
    ///     Отображает список задач с возможностью фильтрации по статусу и типу
    /// </summary>
    /// <param name="state">Фильтр по статусу задачи (New, InProgress, Completed)</param>
    /// <param name="type">Фильтр по типу задачи (Regular, Priority, Repeating)</param>
    /// <returns>Представление со списком задач</returns>
    public async Task<IActionResult> Index(string? state, TaskType? type)
    {
        // Получаем задачи из сервиса
        var taskDtos = await taskApiService.GetTasksAsync(state, type);

        // Преобразуем DTO в ViewModel для представления
        var taskViewModels = taskDtos.Select(t => new GetTask
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Type = t.Type,
            StateName = t.StateName,
        }).ToList();

        return View(taskViewModels);
    }

    /// <summary>
    ///     Отображает форму создания новой задачи (GET)
    /// </summary>
    public IActionResult Create()
        => View();

    /// <summary>
    ///     Обрабатывает отправку формы создания задачи (POST)
    /// </summary>
    /// <param name="model">Данные новой задачи</param>
    /// <returns>Результат создания задачи</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateTask model)
    {
        // Проверяем валидность модели
        if (!ModelState.IsValid)
        {
            // Если данные невалидны, возвращаем форму с ошибками
            return View(model);
        }

        // Преобразуем ViewModel в DTO и создаем задачу через сервис
        await taskApiService.CreateTaskAsync(new CreateTaskDto
        {
            Title = model.Title,
            Description = model.Description,
            Type = model.Type,
        });

        // Перенаправляем на список задач после успешного создания
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    ///     Отображает форму редактирования задачи (GET)
    /// </summary>
    /// <param name="id">Идентификатор задачи для редактирования</param>
    /// <returns>Представление формы редактирования</returns>
    public async Task<IActionResult> Edit(int id)
    {
        // Получаем задачу по ID
        var taskDto = await taskApiService.GetTaskByIdAsync(id);
        if (taskDto == null)
        {
            // Если задача не найдена, возвращаем 404
            return NotFound();
        }

        // Преобразуем TaskDto в EditTask для представления
        var model = new EditTask
        {
            Id = taskDto.Id,
            Title = taskDto.Title,
            Description = taskDto.Description,
            Type = taskDto.Type,
        };

        return View(model);
    }

    /// <summary>
    ///     Обрабатывает отправку формы редактирования задачи (POST)
    /// </summary>
    /// <param name="model">Обновленные данные задачи</param>
    /// <returns>Результат обновления задачи</returns>
    [HttpPost]
    public async Task<IActionResult> Edit(EditTask model)
    {
        // Проверяем валидность модели
        if (!ModelState.IsValid)
        {
            // Если данные невалидны, возвращаем форму с ошибками
            return View(model);
        }

        try
        {
            // Преобразуем ViewModel в DTO и обновляем задачу через сервис
            await taskApiService.UpdateTaskAsync(new UpdateTaskDto
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Type = model.Type,
            });

            // Перенаправляем на список задач после успешного обновления
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException)
        {
            // Если задача не найдена (ArgumentException из сервиса), возвращаем 404
            return NotFound();
        }
    }

    /// <summary>
    ///     Отображает страницу подтверждения удаления задачи (GET)
    /// </summary>
    /// <param name="id">Идентификатор задачи для удаления</param>
    /// <returns>Представление подтверждения удаления</returns>
    public async Task<IActionResult> Delete(int id)
    {
        // Получаем задачу по ID
        var taskDto = await taskApiService.GetTaskByIdAsync(id);
        if (taskDto == null)
        {
            // Если задача не найдена, возвращаем 404
            return NotFound();
        }

        // Преобразуем TaskDto в GetTask для представления подтверждения удаления
        var viewModel = new GetTask
        {
            Id = taskDto.Id,
            Title = taskDto.Title,
            Description = taskDto.Description,
            Type = taskDto.Type,
            StateName = taskDto.StateName,
        };

        return View(viewModel);
    }

    /// <summary>
    ///     Обрабатывает подтверждение удаления задачи (POST)
    /// </summary>
    /// <param name="id">Идентификатор задачи для удаления</param>
    /// <returns>Результат удаления задачи</returns>
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // Удаляем задачу через сервис
        await taskApiService.DeleteTaskAsync(id);

        // Перенаправляем на список задач после успешного удаления
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    ///     Переводит задачу в следующий статус (New → InProgress → Completed)
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <returns>Результат изменения статуса</returns>
    public async Task<IActionResult> NextState(int id)
    {
        try
        {
            // Переводим задачу в следующий статус через сервис
            await taskApiService.MoveToNextStateAsync(id);

            // Перенаправляем на список задач после успешного изменения статуса
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException)
        {
            // Если задача не найдена (ArgumentException из сервиса), возвращаем 404
            return NotFound();
        }
    }
}