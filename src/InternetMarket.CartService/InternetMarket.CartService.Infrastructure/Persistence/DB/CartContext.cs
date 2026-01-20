using Microsoft.EntityFrameworkCore;
using InternetMarket.CartService.Domain.Entities;

namespace InternetMarket.CartService.Infrastructure;

public class CartContext : DbContext
{
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public CartContext(DbContextOptions<CartContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CartConfiguration());
    }
}
