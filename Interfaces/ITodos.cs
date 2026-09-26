using TodoList.Data.Models;
using TodoList.DTOs;

namespace TodoList.Interfaces;

public interface ITodos
{
    public Task<List<Todo>> AllTodos();
    public Task<Todo> OneTodo(int id);
    public Task<Todo> CreateTodo(TodoDto dto);
    public Task<Todo> UpdateTodo(int id, TodoDto newTodo);
    public Task<bool> DeleteTodo(int id);
}
