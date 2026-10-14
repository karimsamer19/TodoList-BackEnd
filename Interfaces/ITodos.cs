using System.Security.Claims;
using TodoList.DTOs;

namespace TodoList.Interfaces;

public interface ITodos
{
    public Task<List<TodoDto>> AllTodos(ClaimsPrincipal principal);
    public Task<TodoDto?> OneTodo(ClaimsPrincipal principal, int id);
    public Task<TodoDto?> CreateTodo(ClaimsPrincipal principal, TodoDto dto);
    public Task<TodoDto?> UpdateTodo(ClaimsPrincipal principal, int id, TodoDto newTodo);
    public Task<bool> DeleteTodo(ClaimsPrincipal principal, int id);
}
