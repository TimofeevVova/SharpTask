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
            _db.Add(task);
            _db.SaveChanges();
        }

        // редактировать 
        public bool Update(TodoTask updatedTask)
        {
            _db.Tasks.Update(updatedTask); // EF сам найдет задачу по Id и подготовит обновление
            var changed = _db.SaveChanges(); // РЕАЛЬНОЕ сохранение в БД
            return changed > 0; // Если хоть одна строка изменилась, вернет true
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
