using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.Entities;
using InternetMarket.ProductService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.ProductService.Infrastructure.Persistance.DB.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ProductName)
                .HasConversion(
                    productName => productName.Value,
                    value => ProductName.Create(value))
                .HasMaxLength(60);
            builder.Property(p => p.Description)
                .HasConversion(
                    description => description.Value,
                    value => Description.Create(value))
                .HasMaxLength(550);
            builder.Property(p => p.Quantity)
                .HasConversion(
                    quantity => quantity.Value,
                    value => Quantity.Create(value))
                .IsRequired();
            builder.Property(p => p.Price)
                .HasConversion(
                    price => price.Value,
                    value => Price.Create(value))
                .IsRequired();
            builder.HasOne(p => p.Provider)
                .WithMany()
                .HasForeignKey(p => p.ProviderId);
            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);
        }
    }
}