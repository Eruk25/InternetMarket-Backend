using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Domain.Entities;
using InternetMarket.PaymentService.Domain.ValueObjects;
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
                Id = PaymentConstants.CardId,
                Name = "Банковская карта",
                SystemName = "Card",
                IsActive = true
            },
            new
            {
                Id = PaymentConstants.CashId,
                Name = "Наличные",
                SystemName = "Cash",
                IsActive = true
            });
        }
    }
}