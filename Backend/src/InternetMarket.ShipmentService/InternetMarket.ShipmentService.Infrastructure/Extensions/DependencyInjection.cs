using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Application.Abstractions.Clients;
using InternetMarket.ShipmentService.Application.Abstractions.Repositories;
using InternetMarket.ShipmentService.Application.Abstractions.UnitOfWork;
using InternetMarket.ShipmentService.Application.Consumers;
using InternetMarket.ShipmentService.Infrastructure.Implementations.Clients;
using InternetMarket.ShipmentService.Infrastructure.Implementations.Repositories;
using InternetMarket.ShipmentService.Infrastructure.Implementations.UnitOfWork;
using InternetMarket.ShipmentService.Infrastructure.Persistence.DB;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetMarket.ShipmentService.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionSection = configuration.GetSection("ConnectionStrings");
            var connectionString = connectionSection["DefaultConnection"];
            services.AddDbContext<ShipmentContext>(option =>
                option.UseSqlServer(connectionString));

            services.Configure<CdekOptions>(configuration.GetSection("ShipmentService"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IShipmentRepository, ShipmentRepository>();
            services.AddScoped<PackagePacker>();
            services.AddHttpClient<IShipmentClient, ShipmentClient>(client =>
            {
                var shipmentSection = configuration.GetSection("ShipmentService");
                client.BaseAddress = new Uri(shipmentSection["BaseUrl"]!);
            });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("Redis")["Configuration"] ?? "127.0.0.1:6379";
                options.InstanceName = "local";
            });
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedConsumer>();
                x.AddEntityFrameworkOutbox<ShipmentContext>(o =>
                {
                    o.UseSqlServer();

                    o.UseBusOutbox();

                    o.QueryDelay = TimeSpan.FromSeconds(5);

                    o.DuplicateDetectionWindow = TimeSpan.FromMinutes(30);
                });
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("shipment-service-order-created", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedConsumer>(context);
                    });
                    var rabbitSection = configuration.GetSection("RabbitMq");
                    cfg.Host(rabbitSection["Host"] ?? "localhost", "/", h =>
                    {
                        h.Username(rabbitSection["Username"] ?? "guest");
                        h.Password(rabbitSection["Password"] ?? "guest");
                    });
                });
            });
            return services;
        }
    }
}