using TodoList.Data.Models;
using TodoList.DTOs;

namespace TodoList.Interfaces;

public interface IUser
{
    public Task<List<User>> AllUsers();
    public Task<User> OneUser(int id);
    public Task<User> UpdateUser(int id, User newTodo);
    public Task<bool> DeleteUser(int id);
}
