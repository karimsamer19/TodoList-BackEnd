using Microsoft.AspNetCore.Identity;

namespace TodoList.Data.Models;

public class User : IdentityUser
{
    public ICollection<Todo> Todos { get; set; } = new List<Todo>();
}
