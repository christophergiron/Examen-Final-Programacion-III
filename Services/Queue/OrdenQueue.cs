using Examen_Final_Programacion_III.Models;
using System.Collections.Concurrent;
namespace Examen_Final_Programacion_III.Services.Queue
{
    public class OrdenQueue : IOrdenQueue
    {
        private readonly ConcurrentQueue<Carrito> _queue = new();

        public void Enqueue(Carrito carrito)
        {
            _queue.Enqueue(carrito);
        }

        public Carrito Dequeue()
        {
            _queue.TryDequeue(out var carrito);
            return carrito;
        }
    }
}
