using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetMarket.EmailService.Infrastructure.Extensions.FluentEmail
{
    public static class AddFluentEmailExtensions
    {
        public static void AddFluentEmail(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            var defaultFromEmail = emailSettings["DefaultFromEmail"];
            var host = emailSettings["SMTPSetting:Host"];
            var port = emailSettings.GetValue<int>("Port");
            var userName = emailSettings["UserName"];
            var password = emailSettings["Password"];

            services.AddFluentEmail(defaultFromEmail)
                .AddSmtpSender(host, port, userName, password);
        }
    }
}