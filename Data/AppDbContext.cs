using Microsoft.EntityFrameworkCore;
using SharpTask.Models;

namespace SharpTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TodoTask> Tasks { get; set; }
    }
}