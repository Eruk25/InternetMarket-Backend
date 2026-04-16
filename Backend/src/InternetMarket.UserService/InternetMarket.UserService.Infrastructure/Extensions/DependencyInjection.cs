using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.PasswordHasher;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Application.Abstractions.TokenGenerator;
using InternetMarket.UserService.Application.Abstractions.UnitOfWork;
using InternetMarket.UserService.Infrastructure.Implementations.JWTGenerator;
using InternetMarket.UserService.Infrastructure.Implementations.PasswordHasher;
using InternetMarket.UserService.Infrastructure.Implementations.UnitOfWork;
using InternetMarket.UserService.Infrastructure.Persistence.DB;
using InternetMarket.UserService.Infrastructure.Persistence.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetMarket.UserService.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("ConnectionStrings");
            var connectionString = section["DefaultConnection"];
            services.AddDbContext<UserContext>(opt =>
                opt.UseSqlServer(connectionString));

            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<UserContext>(o =>
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
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailVerificationTokenRepository, EmailVerificationTokenRepository>();
            services.AddScoped<IResetPasswordTokenRepository, ResetPasswordTokenRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenGenerator, JwtGenerator>();
            services.AddScoped<Identity.PasswordHasher.PasswordHasher>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}