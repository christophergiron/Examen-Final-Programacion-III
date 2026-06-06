using Examen_Final_Programacion_III.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Examen_Final_Programacion_III.Services.Queue.Background
{
    public class OrdenProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOrdenQueue _queue;

        public OrdenProcessor(IServiceScopeFactory scopeFactory, IOrdenQueue queue)
        {
            _scopeFactory = scopeFactory;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var carrito = _queue.Dequeue();

                if (carrito != null)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    decimal total = 0;

                    foreach (var item in carrito.Items)
                    {
                        total += item.Cantidad * item.Product.Price;
                    }

                    Console.WriteLine($"[ASYNC] Orden procesada usuario {carrito.UserId} total {total}");
                }

                await Task.Delay(1000);
            }
        }
    }
}
