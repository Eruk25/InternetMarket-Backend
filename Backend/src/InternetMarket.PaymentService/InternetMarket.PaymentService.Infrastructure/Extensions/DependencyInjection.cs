using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Infrastructure.Persistence.DB;
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

            return services;
        }
    }
}