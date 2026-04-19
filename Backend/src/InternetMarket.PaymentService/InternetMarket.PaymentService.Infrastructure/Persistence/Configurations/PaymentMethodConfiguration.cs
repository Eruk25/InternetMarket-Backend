using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.PaymentService.Infrastructure.Persistence.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(pm => pm.Id);
            builder.Property(pm => pm.Name)
                .HasMaxLength(40)
                .IsRequired();
            builder.Property(pm => pm.SystemName)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(pm => pm.IsActive)
                .IsRequired();
            SeedData(builder);
        }

        public static void SeedData(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasData(new
            {
                Id = Guid.Parse("4dea45ad-4dbf-4ae2-b589-cb442554e357"),
                Name = "Банковская карта",
                SystemName = "Card",
                IsActive = true
            },
            new
            {
                Id = Guid.Parse("f40f776b-49cf-4d0e-b209-bb7a62ca6eb9"),
                Name = "Наличные",
                SystemName = "Cash",
                IsActive = true
            });
        }
    }
}