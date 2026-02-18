using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit.Initializers.PropertyConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InternetMarket.CartService.Infrastructure.Persistence.DB
{
    public class CartContextFactory : IDesignTimeDbContextFactory<CartContext>
    {
        public CartContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(),
                "../InternetMarket.CartService.API");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.Development.json", true)
                .Build();

            var connectionSection = configuration.GetSection("ConnectionStrings");
            var connectionString = connectionSection["DefaultConnection"];

            var optionsBuilder = new DbContextOptionsBuilder<CartContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new CartContext(optionsBuilder.Options);
        }
    }
}