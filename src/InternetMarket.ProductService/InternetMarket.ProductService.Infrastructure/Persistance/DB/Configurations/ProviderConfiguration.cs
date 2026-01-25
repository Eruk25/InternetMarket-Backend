using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.ProductService.Infrastructure.Persistance.DB.Configurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(50);
            builder.Property(p => p.Address)
                .HasMaxLength(80);
            builder.Property(p => p.Email)
                .HasMaxLength(100);
            builder.Property(p => p.NumberPhone)
                .HasMaxLength(50);
        }
    }
}