using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.Entities;
using InternetMarket.ProductService.Domain.ValueObjects.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.ProductService.Infrastructure.Persistance.DB.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CategoryName)
                .HasConversion(
                    categoryName => categoryName.Value,
                    value => CategoryName.Create(value))
                .HasMaxLength(80);
        }
    }
}