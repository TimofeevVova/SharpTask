using System.Security.Cryptography.X509Certificates;

namespace SharpTask.Models
{
    public class TodoTask
    {
        public int Id { get; set; } // айди 
        public string Name { get; set; } // краткое описание 
        public string Description { get; set; } // детальное описание
        public DateTime CreatedDate { get; set; } = DateTime.Now; // дата создания
        public DateTime EndTime { get; set; } // дата до которой нужно завершить задачу
        public bool IsOverdue { get; set; } = false; // true если просрочено 
        public string Creator {  get; set; } // Создатель
        public string Executor { get; set; } // Исполнитель
        public string Status { get; set; } // Статус (новая, в работе, выполнена, просрочена)

    }
}
