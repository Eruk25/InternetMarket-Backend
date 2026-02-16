using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Email;
using InternetMarket.Contracts.Events.Password;
using InternetMarket.EmailService.Application.Consumers;
using InternetMarket.EmailService.Application.Consumers.Email;
using InternetMarket.EmailService.Application.Consumers.Password;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace InternetMarket.EmailService.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<EmailChangeRequestedConsumer>();
                x.AddConsumer<UserRegisteredConsumer>();
                x.AddConsumer<PasswordResetRequestedConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}