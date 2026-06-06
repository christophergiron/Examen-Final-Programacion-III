namespace Examen_Final_Programacion_III.DTOs
{
    public class CarritoDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CarritoItemDto> Items { get; set; }
        public decimal Total { get; set; }
    }
}
