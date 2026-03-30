using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpTask.Models;
using SharpTask.Services;

namespace SharpTask.Controllers
{
    [ApiController]
    [Route("api/tasks")] // Путь будет api/tasks
    public class TasksController : ControllerBase
    {
        // Внедряем сервис через конструктор (DI)
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private readonly ITaskService _taskService;

        //  Получение задачи по Id
        [HttpGet("getTaskById/{id}")]
        public ActionResult<TodoTask> GetById(int id)
        {
            var task = _taskService.GetById(id);
            if (task == null) return NotFound();
            return task;
        }

        // Обновление задачи
        [HttpPut("updateTask/{id}")]
        public IActionResult Update(int id, TodoTask task)
        {
            if (id != task.Id) return BadRequest(); // Защита: ID в пути и в объекте должны совпадать

            var result = _taskService.Update(task);
            if (!result) return NotFound(); // Если задачи с таким ID нет

            return NoContent(); // Стандартный ответ при успешном обновлении (204)
        }

        // Удаление задачи
        [HttpDelete("deleteTask/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _taskService.Delete(id);
            if (!result) return NotFound();

            return NoContent();
        }

        // Получение всех задач
        [HttpGet("getAll")]
        public ActionResult<List<TodoTask>> GetAll() => _taskService.GetAll();

        // Создание задачи
        [HttpPost("createTask")]
        public IActionResult Create(TodoTask task)
        {
            _taskService.Add(task);
            return CreatedAtAction(nameof(GetAll), new { id = task.Id }, task);
        }

    }
}
