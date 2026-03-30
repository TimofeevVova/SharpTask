
using Microsoft.EntityFrameworkCore;
using SharpTask.Data;
using SharpTask.Services;

namespace SharpTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ITaskService, SqlTaskService>();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            // ОСТАВЛЯЕМ ТОЛЬКО ЭТОТ БЛОК CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MainPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting(); // Роутинг ПЕРЕД Cors

            app.UseCors("MainPolicy"); // Применяем политику

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
