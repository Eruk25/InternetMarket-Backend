using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.Abstractions.Clients;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using InternetMarket.OrderService.Infrastructure.Implementations.Clients;
using InternetMarket.OrderService.Infrastructure.Implementations.Repositories;
using InternetMarket.OrderService.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetMarket.OrderService.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionSection = configuration.GetSection("ConnectionStrings");
            var connectionString = connectionSection["DefaultConnection"];
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddHttpClient<ICartServiceClient, CartServiceClient>(client =>
            {
                var cartSection = configuration.GetSection("CartService");
                client.BaseAddress = new Uri(cartSection["BaseUrl"]!);
            });

            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<OrderContext>(o =>
                {
                    o.UseSqlServer();

                    o.UseBusOutbox();

                    o.QueryDelay = TimeSpan.FromSeconds(5);

                    o.DuplicateDetectionWindow = TimeSpan.FromMinutes(30);
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });
            });
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}