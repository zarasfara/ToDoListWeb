using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using ToDoList.Application.Services;
using ToDoList.Domain.Repositories;
using ToDoList.Infrastructure;
using ToDoList.Infrastructure.Repositories;

Batteries_V2.Init();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Регистрация сервисов
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    "default",
    "{controller=Tasks}/{action=Index}/{id?}");

app.Run();