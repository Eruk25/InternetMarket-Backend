using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.ProductService.Infrastructure.Persistance.DB.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title)
                .HasMaxLength(60);
            builder.Property(p => p.Description)
                .HasMaxLength(550);
            builder.Property(p => p.Quantity);
            builder.Property(p => p.Price);
            builder.HasOne(p => p.Provider)
                .WithMany()
                .HasForeignKey(p => p.ProviderId);
            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);
        }
    }
}