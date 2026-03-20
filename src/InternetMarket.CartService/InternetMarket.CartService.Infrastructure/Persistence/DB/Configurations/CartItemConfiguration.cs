using InternetMarket.CartService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InternetMarket.CartService.Infrastructure;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.Id);
        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey(ci => ci.CartId);
        builder.Property(ci => ci.ProductId)
            .IsRequired();
        builder.Property(ci => ci.Title)
            .IsRequired();
        builder.Property(ci => ci.Quantity)
            .IsRequired();
        builder.Property(ci => ci.Price)
            .IsRequired();
    }
}
