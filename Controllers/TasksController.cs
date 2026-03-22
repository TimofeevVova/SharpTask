using Microsoft.AspNetCore.Mvc;
using SharpTask.Models;
using SharpTask.Services;

namespace SharpTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Путь будет api/tasks
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        // Обновление задачи: PUT api/tasks/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoTask task)
        {
            if (id != task.Id) return BadRequest(); // Защита: ID в пути и в объекте должны совпадать

            var result = _taskService.Update(task);
            if (!result) return NotFound(); // Если задачи с таким ID нет

            return NoContent(); // Стандартный ответ при успешном обновлении (204)
        }


        // Удаление задачи: DELETE api/tasks/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _taskService.Delete(id);
            if (!result) return NotFound();

            return NoContent();
        }


        // Внедряем сервис через конструктор (DI)
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<List<TodoTask>> GetAll() => _taskService.GetAll();

        [HttpPost]
        public IActionResult Create(TodoTask task)
        {
            _taskService.Add(task);
            return CreatedAtAction(nameof(GetAll), new { id = task.Id }, task);
        }

        // Сюда добавь методы для Update и Delete по аналогии
    }
}
