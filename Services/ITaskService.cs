using SharpTask.Models;

namespace SharpTask.Services
{
    public interface ITaskService
    {
        List<TodoTask> GetAll();
        TodoTask? GetById(int id);
        void Add(TodoTask task);
        bool Update(TodoTask task);
        bool Delete(int id);
    }
}
