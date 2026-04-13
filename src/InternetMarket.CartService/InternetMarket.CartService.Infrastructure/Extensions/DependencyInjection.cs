using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using InternetMarket.CartService.Application.Abstractions.Clients;
using InternetMarket.CartService.Application.Abstractions.Repositories;
using InternetMarket.CartService.Application.Consumers;
using InternetMarket.CartService.Infrastructure.Implementations.Clients;
using InternetMarket.CartService.Infrastructure.Implementations.Repositories;
using MassTransit;
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

            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserRegisteredConsumer>();

                x.AddEntityFrameworkOutbox<CartContext>(o =>
                {
                    o.UseSqlServer();

                    o.UseBusOutbox();

                    o.QueryDelay = TimeSpan.FromSeconds(5);

                    o.DuplicateDetectionWindow = TimeSpan.FromMinutes(30);
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("cart-service-user-registered", e =>
                    {
                        e.ConfigureConsumer<UserRegisteredConsumer>(context);
                    });

                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<ICartRepository, CartRepository>();
            return services;
        }
    }
}