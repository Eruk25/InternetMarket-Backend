using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetMarket.CartService.API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                throw new KeyNotFoundException($"User with id {userId} was not found");

            return Guid.Parse(userId);
        }
    }
}