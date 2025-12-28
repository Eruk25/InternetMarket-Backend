using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.PasswordHasher;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Infrastructure.Implementations.PasswordHasher;
using InternetMarket.UserService.Infrastructure.Persistence.DB;
using InternetMarket.UserService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InternetMarket.UserService.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<UserContext>(opt =>
                opt.UseSqlServer("Server=LENOVO\\SQLEXPRESS03;Database=UserService;Trusted_Connection=True;TrustServerCertificate=True;"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<Identity.PasswordHasher.PasswordHasher>();
            return services;
        }
    }
}