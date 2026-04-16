using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Infrastructure.Implementations.JWTGenerator
{
    public class AuthOptions
    {
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public string SecretKey { get; init; } = string.Empty;
        public TimeSpan ExpireTime { get; init; }
    };
}