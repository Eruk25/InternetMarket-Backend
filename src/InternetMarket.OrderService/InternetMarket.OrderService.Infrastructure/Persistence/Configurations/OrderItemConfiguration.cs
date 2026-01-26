using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.OrderService.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.OrderId)
                .IsRequired();
            builder.Property(oi => oi.ProductId)
                .IsRequired();
            builder.Property(oi => oi.Quantity)
                .IsRequired();
            builder.Property(oi => oi.UntilPrice)
                .IsRequired();
            builder.Property(oi => oi.CreatedAt)
                .IsRequired();
            builder.Property(oi => oi.UpdatedAt);
        }
    }
}