using Examen_Final_Programacion_III.Models;

namespace Examen_Final_Programacion_III.Services.Queue
{
    public interface IOrdenQueue
    {
        void Enqueue(Carrito carrito);
        Carrito Dequeue();
    }
}
