using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.EmailService.Application.Abstractions.EmailSender;
using InternetMarket.EmailService.Application.Consumers;
using InternetMarket.EmailService.Application.Consumers.Email;
using InternetMarket.EmailService.Application.Consumers.Order;
using InternetMarket.EmailService.Application.Consumers.Password;
using InternetMarket.EmailService.Infrastructure.Implementations.EmailSender;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetMarket.EmailService.Infrastructure.Extensions.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<EmailChangeRequestedConsumer>();
                x.AddConsumer<UserRegisteredConsumer>();
                x.AddConsumer<PasswordResetRequestedConsumer>();
                x.AddConsumer<OrderCreatedConsumer>();
                x.AddConsumer<OrderCancelledConsumer>();
                x.AddConsumer<OrderPaidConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("email-service-user-registered", e =>
                    {
                        e.ConfigureConsumer<UserRegisteredConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("email-service-order-created", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedConsumer>(context);
                    });

                    var rabbitSection = configuration.GetSection("RabbitMq");
                    cfg.Host(rabbitSection["Host"] ?? "localhost", "/", h =>
                    {
                        h.Username(rabbitSection["Username"] ?? "guest");
                        h.Password(rabbitSection["Password"] ?? "guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IEmailSender, EmailSender>();
            return services;
        }
    }
}