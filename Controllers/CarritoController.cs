using Examen_Final_Programacion_III.Data;
using Examen_Final_Programacion_III.Services;
using Examen_Final_Programacion_III.Services.Queue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

[ApiController]
[Route("api/[controller]")]
public class CarritoController : ControllerBase
{
    private readonly IOrdenQueue _queue;
    private readonly AppDbContext _context;
    private readonly CarritoService _service;
    public CarritoController(AppDbContext context, IOrdenQueue queue, CarritoService service)
    {
        _context = context;
        _queue = queue;
        _service = service;
    }

    [HttpGet("usuario/{userId}")]
    public async Task<IActionResult> GetCarrito(Guid userId)
    {
        var result = await _service.GetCarritoByUserId(userId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{userId}/add")]
    public async Task<IActionResult> AddProduct(Guid userId, int productId, int cantidad)
    {
        try
        {
            var result = await _service.AddProduct(userId, productId, cantidad);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("checkout-async")]
    public async Task<IActionResult> CheckoutAsync(Guid userId)
    {
        var carrito = await _context.Carritos
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (carrito == null) return NotFound();

        _queue.Enqueue(carrito);

        return Ok("Orden enviada para procesamiento async");
    }
}