using Microsoft.AspNetCore.Mvc;
using TodoList.Interfaces;
namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private ITodos todos;
        public TodosController(ITodos todos)
        {
            this.todos = todos;
        }
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return Ok(await todos.AllTodos());
        }
    }
}
