using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Infrastructure.Persistance.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetMarket.ProductService.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfastructure(IServiceCollection services, IConfiguration configuration)
        {
            var connectionSection = configuration.GetSection("ConnectionStrings");
            var connectionString = connectionSection["DefaultConnection"];

            services.AddDbContext<ProductContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}