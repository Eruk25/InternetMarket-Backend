using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Clients;
using InternetMarket.CartService.Application.Abstractions.Repositories;
using InternetMarket.CartService.Infrastructure.Implementations.Clients;
using InternetMarket.CartService.Infrastructure.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetMarket.CartService.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionSection = configuration.GetSection("ConnectionStrings");
            var connectionString = connectionSection["DefaultConnection"];
            services.AddDbContext<CartContext>(option =>
                option.UseSqlServer(connectionString));

            services.AddHttpClient<IProductServiceClient, ProductServiceClient>(client =>
            {
                var productSection = configuration.GetSection("ProductService");
                client.BaseAddress = new Uri(productSection["BaseUrl"]!);
            });
            services.AddScoped<ICartRepository, CartRepository>();
            return services;
        }
    }
}