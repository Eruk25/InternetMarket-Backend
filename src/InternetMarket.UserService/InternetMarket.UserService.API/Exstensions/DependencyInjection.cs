using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory;
using InternetMarket.UserService.API.Implementations.EmailVerificationLinkFactory;

namespace InternetMarket.UserService.API.Exstensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["AuthOptions:Issuer"],
                        ValidAudience = configuration["AuthOptions:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["AuthOptions"]!)),
                        RoleClaimType = ClaimTypes.Role
                    };
                });
            services.AddAuthorization();
            services.AddScoped<IEmailVerificationLinkFactory, EmailVerificationLinkFactory>();
            return services;
        }
    }
}