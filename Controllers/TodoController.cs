using Microsoft.AspNetCore.Mvc;
using TaskProjectApi.Models;

namespace TaskProjectApi.Controllers
{
    [Route("api/todo")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository<TodoItem> _todoRepository;

        public TodoController(ITodoRepository<TodoItem> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var todoItems = _todoRepository.GetAll();
            return Ok(todoItems);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            var todoItem = _todoRepository.Get(id);

            if (todoItem == null) return NotFound("Not found");

            return Ok(todoItem);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoItem todoItem)
        {
            if (todoItem == null) return BadRequest("data is null.");

            _todoRepository.Add(todoItem);
            return CreatedAtRoute(
                "Get",
                new {todoItem.Id},
                todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] TodoItem todoItem)
        {
            if (todoItem == null) return BadRequest("todo is null.");

            var updateToUpdate = _todoRepository.Get(id);
            if (updateToUpdate == null) return NotFound("Todo record couldn't be found.");

            _todoRepository.Update(updateToUpdate, todoItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todoItem = _todoRepository.Get(id);
            if (todoItem == null) return NotFound("Todo record couldn't be found.");

            _todoRepository.Delete(todoItem);
            return NoContent();
        }
    }
}