namespace Examen_Final_Programacion_III.Models
{
    public class Carrito
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User user { get; set; }

        public List<CarritoItems> Items { get; set; } = new List<CarritoItems>();
    }
}
