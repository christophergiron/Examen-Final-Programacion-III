namespace Examen_Final_Programacion_III.DTOs
{
    public class CarritoItemDto
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
