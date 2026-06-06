using Examen_Final_Programacion_III.Data;
using Examen_Final_Programacion_III.Services;
using Examen_Final_Programacion_III.Services.Queue;
using Examen_Final_Programacion_III.Services.Queue.Background;
using Microsoft.EntityFrameworkCore;

namespace Examen_Final_Programacion_III
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            builder.Services.AddSingleton<IOrdenQueue, OrdenQueue>();
            builder.Services.AddHostedService<OrdenProcessor>();
            builder.Services.AddScoped<ICarritoService, CarritoService>();
            builder.Services.AddScoped<CarritoService>();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
