using Microsoft.AspNetCore.Mvc;
using TodoList.DTOs;
using TodoList.Interfaces;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth auth;
        public AuthController(IAuth authService)
        {
            auth = authService;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {

            if (await auth.Register(dto))
            {
                return Ok("User Added :)");
            }
            return BadRequest("Data Invalid please check your data");
        }

        [HttpPost("login")]
        public async Task<IActionResult > Login([FromBody] LoginDto dto)
        {
            var token = await auth.Login(dto);

            if (token != null)
            {
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
