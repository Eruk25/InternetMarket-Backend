using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Domain.Entities;
using InternetMarket.ShipmentService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.ShipmentService.Infrastructure.Persistence.DB.Configurations
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.OrderId)
                .IsRequired();
            builder.OwnsOne(s => s.FullName, navigatorBuilder =>
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
            builder.OwnsOne(s => s.Location, navigatorBuilder =>
            {
                navigatorBuilder.Property(l => l.City)
                    .HasColumnName("City")
                    .IsRequired(true)
                    .HasMaxLength(50);
                navigatorBuilder.Property(l => l.Address)
                    .HasColumnName("Address")
                    .IsRequired(true)
                    .HasMaxLength(50);
            });
            builder.Property(s => s.NumberPhone)
                .HasConversion(
                    numberPhone => numberPhone.Value,
                    value => NumberPhone.Create(value));
            builder.Property(s => s.Status)
                .HasConversion(
                    status => status.Value,
                    value => Status.FromValue(value));
            builder.Property(s => s.ShipmentAmount)
                .IsRequired(true);
        }
    }
}