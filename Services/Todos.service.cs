using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Data.Models;
using TodoList.DTOs;
using TodoList.Interfaces;

namespace TodoList.Services;

public class TodosService : ITodos
{
    private readonly AppDbContext db;
    private readonly UserManager<User> userManager;
    public TodosService(AppDbContext _db, UserManager<User> userManager)
    {
        db = _db;
        this.userManager = userManager;
    }

    public async Task<List<TodoDto>> AllTodos(ClaimsPrincipal principal)
    {
        string? userId = userManager.GetUserId(principal);

        if (userId == null)
            return new List<TodoDto>();

        User? user = await db.Users
            .Include(u => u.Todos)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return new List<TodoDto>();

        return user.Todos
            .Select(FillTodoDto)
            .ToList();
    }

    public async Task<TodoDto?> OneTodo(ClaimsPrincipal principal, int id)
    {
        User? user = await userManager.GetUserAsync(principal);

        if (user == null)
            return null;

        Todo? todo = await db.todos
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

        if (todo == null)
            return null;

        return FillTodoDto(todo);
    }

    public async Task<TodoDto?> CreateTodo(ClaimsPrincipal principal, TodoDto dto)
    {
        User? user = await userManager.GetUserAsync(principal);

        if (user == null)
            return null;

        Todo todo = new()
        {
            Title = dto.Title,
            Description = dto.Description,
            UserId = user.Id,
        };

        await db.todos.AddAsync(todo);
        await db.SaveChangesAsync();
        return FillTodoDto(todo);
    }

    public async Task<TodoDto?> UpdateTodo(ClaimsPrincipal principal, int id, TodoDto newTodo)
    {
        User? user = await userManager.GetUserAsync(principal);

        if (user == null)
            return null;

        Todo? todo = await db.todos
        .FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

        if (todo == null)
            return null;

        db.Entry(todo).CurrentValues.SetValues(newTodo);

        await db.SaveChangesAsync();

        return FillTodoDto(todo);
    }
    public async Task<bool> DeleteTodo(ClaimsPrincipal principal, int id)
    {
        var todo = await db.todos.FindAsync(id);

        if (todo == null)
            return false;

        User? user = await userManager.GetUserAsync(principal);

        if (user == null || user.Id != todo?.UserId)
            return false;

        db.todos.Remove(todo);
        await db.SaveChangesAsync();

        return true;
    }
    //? Private Methods
    private TodoDto FillTodoDto(Todo todo)
    {
        return new TodoDto()
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            isCompleted = todo.isCompleted,
        };
    }

}
