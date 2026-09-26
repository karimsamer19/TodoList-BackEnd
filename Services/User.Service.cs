using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Data.Models;
using TodoList.Interfaces;

namespace TodoList.Services;

public class UserService : IUser
{
    public AppDbContext db;
    public UserService(AppDbContext context)
    {
        db = context;
    }
    public async Task<List<User>> AllUsers()
    {
        return await db.Users.ToListAsync();
    }
    public Task<User> UpdateUser(int id, User newTodo)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> OneUser(int id)
    {
        throw new NotImplementedException();
    }
}
