using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Data;
using ToDoList.Api.Models;
using TaskFactory = ToDoList.Api.Models.TaskFactory;

namespace ToDoList.Api.Controllers;

public class TasksController : Controller
{
    private readonly ToDoListContext _context;
    private readonly TaskFactory _factory = new();

    public TasksController(ToDoListContext context)
    {
        _context = context;
    }

    // LIST
    public async Task<IActionResult> Index(string? state, TaskType? type)
    {
        var query = _context.Tasks.AsQueryable();

        if (!string.IsNullOrEmpty(state))
        {
            query = query.Where(t => t.StateName == state);
        }

        if (type != null)
        {
            query = query.Where(t => t.Type == type);
        }

        var tasks = await query.ToListAsync();
        return View(tasks);
    }

    // CREATE GET
    public IActionResult Create()
    {
        return View();
    }

    // CREATE POST
    [HttpPost]
    public async Task<IActionResult> Create(string title, string description, TaskType type)
    {
        var task = _factory.CreateTask(type);
        task.Title = title;
        task.Description = description;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // EDIT GET
    public async Task<IActionResult> Edit(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // EDIT POST
    [HttpPost]
    public async Task<IActionResult> Edit(TaskItem updated)
    {
        var task = await _context.Tasks.FindAsync(updated.Id);
        if (task == null)
        {
            return NotFound();
        }

        task.Title = updated.Title;
        task.Description = updated.Description;
        task.Type = updated.Type;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // DELETE GET
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }
    
    // DELETE POST
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
    
    // NEXT STATE
    public async Task<IActionResult> NextState(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        
        task.SyncStateObject();
        task.MoveToNextState();

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}