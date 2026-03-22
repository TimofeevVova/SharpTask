using SharpTask.Models;

namespace SharpTask.Services
{
    public class TaskService : ITaskService
    {
        // список-заменитель БД. Static нужен, чтобы данные не удалялись при каждом запросе
        private static readonly List<TodoTask> _tasks = new();
        private static int _nextId = 1;

        public List<TodoTask> GetAll() => _tasks;

        // получить по id
        public TodoTask? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

        // создать
        public void Add(TodoTask task)
        {
            task.Id = _nextId++;
            _tasks.Add(task);
        }

        // редактировать 
        public bool Update(TodoTask updatedTask)
        {
            var index = _tasks.FindIndex(t => t.Id == updatedTask.Id);
            if (index == -1) return false;

            _tasks[index] = updatedTask;
            return true;
        }

        // удалить 
        public bool Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return false;

            _tasks.Remove(task);
            return true;
        }
    }
}
