using Microsoft.EntityFrameworkCore;
using Examen_Final_Programacion_III.Models;
namespace Examen_Final_Programacion_III.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoItems> CarritoItems { get; set; }
    }
}
