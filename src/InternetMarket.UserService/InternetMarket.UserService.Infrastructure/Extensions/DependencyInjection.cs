using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.PasswordHasher;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Application.Abstractions.TokenGenerator;
using InternetMarket.UserService.Infrastructure.Implementations.JWTGenerator;
using InternetMarket.UserService.Infrastructure.Implementations.PasswordHasher;
using InternetMarket.UserService.Infrastructure.Persistence.DB;
using InternetMarket.UserService.Infrastructure.Persistence.Repositories;
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
            services.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailVerificationTokenRepository, EmailVerificationTokenRepository>();
            services.AddScoped<IResetPasswordTokenRepository, ResetPasswordTokenRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenGenerator, JwtGenerator>();
            services.AddScoped<Identity.PasswordHasher.PasswordHasher>();
            return services;
        }
    }
}