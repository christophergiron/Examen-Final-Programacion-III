namespace Examen_Final_Programacion_III.Models
{
    public class CarritoItems
    {
        public Guid Id { get; set; }
        public Guid CarritoId { get; set; }
        public Carrito Carrito { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Cantidad { get; set; }
    }
}
