using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.Abstractions.Clients;
using InternetMarket.PaymentService.Application.Abstractions.PaymentGateway;
using InternetMarket.PaymentService.Application.Abstractions.Repositories;
using InternetMarket.PaymentService.Application.Abstractions.UnitOfWork;
using InternetMarket.PaymentService.Infrastructure.Implementations.Clients;
using InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid;
using InternetMarket.PaymentService.Infrastructure.Implementations.Repositories;
using InternetMarket.PaymentService.Infrastructure.Implementations.UnitOfWork;
using InternetMarket.PaymentService.Infrastructure.Persistence.DB;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetMarket.PaymentService.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var paymentSection = configuration.GetSection("ConnectionStrings");
            var connectionString = paymentSection["DefaultConnection"];

            services.AddDbContext<PaymentContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddHttpClient<IOrderServiceClient, OrderServiceClient>(client =>
            {
                var orderSection = configuration.GetSection("OrderService");
                client.BaseAddress = new Uri(orderSection["BaseUrl"]!);
            });
            services.AddHttpClient<IPaymentGateway, BePaidClient>(client =>
            {
                var bepaidSection = configuration.GetSection("BepaidService");
                client.BaseAddress = new Uri(bepaidSection["BaseUrl"]!);
            });

            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<PaymentContext>(o =>
                {
                    o.UseSqlServer();

                    o.UseBusOutbox();

                    o.QueryDelay = TimeSpan.FromSeconds(5);
                    o.DuplicateDetectionWindow = TimeSpan.FromMinutes(30);
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitSection = configuration.GetSection("RabbitMq");
                    cfg.Host(rabbitSection["Host"] ?? "localhost", "/", h =>
                    {
                        h.Username(rabbitSection["Username"] ?? "guest");
                        h.Password(rabbitSection["Password"] ?? "guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            return services;
        }
    }
}