using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.Data.Models;
using TodoList.DTOs;
using TodoList.Interfaces;

namespace TodoList.Services;

public class UserService : IUser
{
    private readonly UserManager<User> userManager;
    public UserService(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }
    public async Task<List<UserDto>> AllUsers()
    {
        List<User> users = await userManager.Users
       .Include(user => user.Todos)
       .ToListAsync();
        return users.Select(FillUserDto).ToList();
    }
    public async Task<UserDto?> FindUser(string username)
    {
        User? user = await userManager.Users
            .Include(user => user.Todos)
            .FirstOrDefaultAsync(user => user.UserName == username);

        if (user == null)
            return null;

        UserDto userDto = FillUserDto(user);
        return userDto;
    }
    public async Task<UserDto?> GetCurrentUser(ClaimsPrincipal principlal)
    {
        string? userId = userManager.GetUserId(principlal);
        User? user1 = await userManager.Users.Include(user => user.Todos).FirstOrDefaultAsync(user => user.Id == userId);

        if (user1 == null)
            return null;
        return FillUserDto(user1);
    }
    public async Task<bool> UpdateUser(string username, UpdateUserDto dto)
    {
        User? user = await userManager.FindByNameAsync(username);

        if (user == null)
            return false;

        if (dto.Username != null)
        {
            var result = await userManager.SetUserNameAsync(user, dto.Username);

            if (!result.Succeeded)
                return false;
        }

        if (dto.Email != null)
        {
            var result = await userManager.SetEmailAsync(user, dto.Email);

            if (!result.Succeeded)
                return false;
        }

        if (dto.PhoneNumber != null)
        {
            user.PhoneNumber = dto.PhoneNumber;
        }

        var updateResult = await userManager.UpdateAsync(user);

        return updateResult.Succeeded;
    }

    public async Task<bool> DeleteUser(string username)
    {
        User? user = await userManager.FindByNameAsync(username);

        if (user == null)
            return false;

        var result = await userManager.DeleteAsync(user);
        return result.Succeeded;
    }


    public async Task<bool> IsAuthorizedForUser(ClaimsPrincipal principal, string username)
    {
        User? currentUser = await userManager.GetUserAsync(principal);

        if (currentUser == null)
            return false;

        return currentUser.UserName == username;
    }

    //? Private Methods
    private UserDto FillUserDto(User user)
    {
        return new UserDto
        {
            username = user.UserName!,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            todos = user.Todos.Select(todo => new TodoDto()
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
            }).ToList()
        };
    }

}
