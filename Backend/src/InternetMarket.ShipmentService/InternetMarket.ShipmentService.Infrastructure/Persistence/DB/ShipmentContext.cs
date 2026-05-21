using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Domain.Entities;
using InternetMarket.ShipmentService.Infrastructure.Persistence.DB.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InternetMarket.ShipmentService.Infrastructure.Persistence.DB
{
    public class ShipmentContext : DbContext
    {
        public DbSet<Shipment> Shipments { get; set; }

        public ShipmentContext(DbContextOptions<ShipmentContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShipmentConfiguration());
        }
    }
}