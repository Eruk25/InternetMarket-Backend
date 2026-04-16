using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                throw new KeyNotFoundException($"User was not found");

            return Guid.Parse(userId);
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;

            if (email is null)
                throw new Exception($"Email was not found");

            return email;
        }
    }
}