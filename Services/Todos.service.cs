using System;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Data.Models;
using TodoList.DTOs;
using TodoList.Interfaces;

namespace TodoList.Services;

public class TodosService : ITodos
{
    private readonly AppDbContext db;
    public TodosService(AppDbContext _db)
    {
        db = _db;
    }

    public async Task<List<Todo>> AllTodos()
    {
        return await db.todos.ToListAsync();
    }

    public async Task<Todo> OneTodo(int id)
    {
        return await db.todos.FindAsync(id);
    }

    public async Task<Todo> CreateTodo(TodoDto dto)
    {
        Todo todo = new()
        {
            Title = dto.Title,
            Description = dto.Description,
            UserId = dto.UserId,
        };

        await db.todos.AddAsync(todo);
        await db.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo> UpdateTodo(int id, TodoDto newTodo)
    {
        var todo = await db.todos.FindAsync(id);

        if (todo != null)
        {
            db.Entry(todo).CurrentValues.SetValues(newTodo);
            await db.SaveChangesAsync();
        }
        return todo;
    }
    public async Task<bool> DeleteTodo(int id)
    {
        var todo = await db.todos.FindAsync(id);

        if (todo != null)
        {
            db.todos.Remove(todo);
            await db.SaveChangesAsync();
            return true;
        }
        return false;
    }


}
