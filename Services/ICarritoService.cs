using Examen_Final_Programacion_III.DTOs;

namespace Examen_Final_Programacion_III.Services
{
    public interface ICarritoService
    {
        Task<CarritoDto> GetCarritoByUserId(Guid userId);
        Task<CarritoDto> AddProduct(Guid userId, int productId, int cantidad);
        Task<object> Checkout(Guid userId);
    }
}
