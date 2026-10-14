using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.DTOs;
using TodoList.Interfaces;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        public readonly IUser userService;
        public UsersController(IUser userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await userService.AllUsers());
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            UserDto? user = await userService.GetCurrentUser(User);
            if (user == null)
                return Unauthorized("Your sessions had expire please Login again and come after that");

            return Ok(user);
        }
        [HttpPatch("me")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto userDto)
        {
            UserDto? user = await userService.GetCurrentUser(User);
            if (user == null)
                return Unauthorized("Your sessions had expire please Login again and come after that");

            if (!await userService.UpdateUser(user.username, userDto))
                return Forbid();

            user = await userService.GetCurrentUser(User);
            return Ok(user);
        }
    }
}
