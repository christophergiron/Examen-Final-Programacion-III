using Examen_Final_Programacion_III.Data;
using Examen_Final_Programacion_III.DTOs;
using Examen_Final_Programacion_III.Models;
using Examen_Final_Programacion_III.Services;
using Microsoft.EntityFrameworkCore;

public class CarritoService : ICarritoService
{
    private readonly AppDbContext _context;

    public CarritoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CarritoDto> GetCarritoByUserId(Guid userId)
    {
        var carrito = await _context.Carritos
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (carrito == null) return null;

        return MapToDto(carrito);
    }

    public async Task<CarritoDto> AddProduct(Guid userId, int productId, int cantidad)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
            throw new Exception("Producto no existe");

        int refillId = 1;
        if (product.Id == refillId && product.Stock < 5000)
        {
            product.Stock = 5000;
        }

        if (product.Stock < cantidad)
            throw new Exception("No hay suficiente stock");

        var carrito = await _context.Carritos
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (carrito == null)
        {
            carrito = new Carrito
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Items = new List<CarritoItems>()
            };

            _context.Carritos.Add(carrito);
        }

        var item = carrito.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            item.Cantidad += cantidad;
        }
        else
        {
            var nuevoItem = new CarritoItems
            {
                Id = Guid.NewGuid(),
                CarritoId = carrito.Id,
                ProductId = productId,
                Cantidad = cantidad
            };

            _context.CarritoItems.Add(nuevoItem);
        }

        await _context.SaveChangesAsync();

        return MapToDto(carrito);
    }

    public async Task<object> Checkout(Guid userId)
    {
        var carrito = await _context.Carritos
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (carrito == null || !carrito.Items.Any())
            throw new Exception("Carrito vacío");

        foreach (var item in carrito.Items)
        {
            if (item.Product.Stock < item.Cantidad)
                throw new Exception($"No hay stock de {item.Product.Name}");
        }

        foreach (var item in carrito.Items)
        {
            item.Product.Stock -= item.Cantidad;
        }

        var total = carrito.Items.Sum(i => i.Cantidad * i.Product.Price);

        carrito.Items.Clear();

        await _context.SaveChangesAsync();

        _ = Task.Run(() =>
        {
            Console.WriteLine($"Orden procesada usuario {userId} total {total}");
        });

        return new
        {
            mensaje = "Compra realizada",
            total = total
        };
    }

    private CarritoDto MapToDto(Carrito carrito)
    {
        return new CarritoDto
        {
            Id = carrito.Id,
            UserId = carrito.UserId,
            Items = carrito.Items.Select(i => new CarritoItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                NombreProducto = i.Product?.Name,
                Cantidad = i.Cantidad
            }).ToList(),
            Total = carrito.Items.Sum(i => i.Cantidad * i.Product.Price)
        };
    }
}
