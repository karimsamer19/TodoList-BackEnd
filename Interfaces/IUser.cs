using System.Security.Claims;
using TodoList.DTOs;

namespace TodoList.Interfaces;

public interface IUser
{
    public Task<List<UserDto>> AllUsers();
    public Task<UserDto> FindUser(string username);
    public Task<bool> UpdateUser(string username, UpdateUserDto newTodo);
    public Task<bool> DeleteUser(string username);
    Task<UserDto?> GetCurrentUser(ClaimsPrincipal user);
    Task<bool> IsAuthorizedForUser(ClaimsPrincipal principal, string username);

}
