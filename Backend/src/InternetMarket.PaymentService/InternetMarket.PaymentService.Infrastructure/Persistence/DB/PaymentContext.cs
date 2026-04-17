using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Domain.Entities;
using InternetMarket.PaymentService.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InternetMarket.PaymentService.Infrastructure.Persistence.DB
{
    public class PaymentContext(DbContextOptions<PaymentContext> options) : DbContext(options)
    {
        DbSet<Transaction> Transactions { get; set; }
        DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}