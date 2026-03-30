using SharpTask.Data;
using SharpTask.Models;

namespace SharpTask.Services
{
    public class SqlTaskService : ITaskService
    {
        private readonly AppDbContext _db; // Наш мост к базе

        public SqlTaskService(AppDbContext db)
        {
            _db = db;
        }

        //полуячение списка
        public List<TodoTask> GetAll()
        { 
            return _db.Tasks.ToList();
        }

        // получить по id
        public TodoTask? GetById(int id)
        {
            return _db.Tasks.FirstOrDefault(t => t.Id == id);
        }

        // создать
        public void Add(TodoTask task)
        {
            // Принудительно помечаем даты как UTC
            task.CreatedDate = DateTime.SpecifyKind(task.CreatedDate, DateTimeKind.Utc);

            if (task.EndTime != default)
            {
                task.EndTime = DateTime.SpecifyKind(task.EndTime, DateTimeKind.Utc);
            }

            _db.Tasks.Add(task);
            _db.SaveChanges();
        }

        // редактировать 
        public bool Update(TodoTask updatedTask)
        {
            // Принудительно ставим пометку UTC для дат
            updatedTask.CreatedDate = DateTime.SpecifyKind(updatedTask.CreatedDate, DateTimeKind.Utc);
            updatedTask.EndTime = DateTime.SpecifyKind(updatedTask.EndTime, DateTimeKind.Utc);

            _db.Tasks.Update(updatedTask);
            return _db.SaveChanges() > 0;
        }

        // удалить 
        public bool Delete(int id)
        {
            var task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return false;

            _db.Tasks.Remove(task);
            return _db.SaveChanges() > 0;
        }
    }
}
