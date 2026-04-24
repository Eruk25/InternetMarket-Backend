using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Domain.Entities;
using InternetMarket.OrderService.Domain.ValueObjects;
using InternetMarket.UserService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.OrderService.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.OwnsOne(u => u.FullName, navigatorBuilder =>
            {
                navigatorBuilder.Property(n => n.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired()
                    .HasMaxLength(50);
                navigatorBuilder.Property(n => n.LastName)
                    .HasColumnName("LastName")
                    .IsRequired()
                    .HasMaxLength(50);
            });
            builder.Property(u => u.Email)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value))
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}