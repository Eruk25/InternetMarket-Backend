using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Domain.Entities;
using InternetMarket.PaymentService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetMarket.PaymentService.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Amount)
                .IsRequired();
            builder.Property(t => t.PaymentId)
                .IsRequired();
            builder.HasOne(t => t.PaymentMethod)
                .WithMany()
                .HasForeignKey(t => t.PaymentId)
                .IsRequired();
            builder.Property(t => t.Status)
                .HasConversion(
                    status => status.Value,
                    value => Status.FromValue(value));
        }
    }
}