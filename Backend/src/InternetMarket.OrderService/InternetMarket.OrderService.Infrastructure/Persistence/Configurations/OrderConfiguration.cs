using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Domain.Entities;
using InternetMarket.OrderService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;

namespace InternetMarket.OrderService.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.UserId)
                .IsRequired();
            builder.OwnsOne(o => o.CustomerName, navigatorBuilder =>
            {
                navigatorBuilder.Property(n => n.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired(true)
                    .HasMaxLength(50);
                navigatorBuilder.Property(n => n.LastName)
                    .HasColumnName("LastName")
                    .IsRequired(true)
                    .HasMaxLength(50);
            });
            builder.Property(o => o.PaymentMethod)
                .HasConversion(
                    paymentMethod => paymentMethod.Value,
                    value => PaymentMethod.FromValue(value))
                .IsRequired(true);
            builder.OwnsOne(o => o.DeliveryInfo, navigationBulder =>
            {
                navigationBulder.Property(di => di.DeliveryType)
                    .HasColumnName("DeliveryType")
                    .IsRequired(true);
                navigationBulder.Property(di => di.ToCityCode)
                    .HasColumnName("ToCityCode");
                navigationBulder.Property(di => di.DeliveryPointId)
                    .HasColumnName("DeliveryPointId");
                navigationBulder.Property(di => di.City)
                    .HasColumnName("City");
                navigationBulder.Property(di => di.Address)
                    .HasColumnName("Address");
            });
            builder.Property(p => p.CustomerPhone)
                .HasConversion(
                    numberPhone => numberPhone.Value,
                    value => NumberPhone.Create(value))
                .HasMaxLength(50)
                .IsRequired(true);
            builder.Property(o => o.TotalPrice)
                .IsRequired(true);
            builder.Property(o => o.PaymentDate)
                .IsRequired(false);
            builder.Property(o => o.Status)
                .HasConversion(
                    status => status.Value,
                    value => OrderStatus.FromValue(value)
                )
                .IsRequired(true);
            builder.Property(o => o.CreatedAt)
                .IsRequired(true);
            builder.Property(o => o.UpdatedAt);
            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Metadata
                .FindNavigation(nameof(Order.OrderItems))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}