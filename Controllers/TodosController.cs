using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.DTOs;
using TodoList.Interfaces;
namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private ITodos todoManger;
        public TodosController(ITodos todos)
        {
            todoManger = todos;
        }
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return Ok(await todoManger.AllTodos(User));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodos([FromRoute] int id)
        {
            TodoDto? todo = await todoManger.OneTodo(User, id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoDto todoDto)
        {
            TodoDto? todo = await todoManger.CreateTodo(User, todoDto);
            if (todo == null)
                return Forbid();

            return Ok(todo);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] int id, [FromBody] TodoDto todoDto)
        {
            TodoDto? todo = await todoManger.UpdateTodo(User, id, todoDto);
            if (todo == null)
                return Forbid();
            return Ok(todo);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {

            if (!await todoManger.DeleteTodo(User, id))
                return NotFound();
            return Ok("Delete request has successfuly");
        }
    }
}
