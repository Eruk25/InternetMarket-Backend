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
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .HasConversion(
                    providerName => providerName.Value,
                    value => ProviderName.Create(value))
                .HasMaxLength(50)
                .IsRequired();
            builder.ComplexProperty(p => p.Address)
                .IsRequired();
            builder.Property(p => p.Email)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value))
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.NumberPhone)
                .HasConversion(
                    numberPhone => numberPhone.Value,
                    value => NumberPhone.Create(value))
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}