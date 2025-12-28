using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Infrastructure.Implementations.JWTGenerator
{
    public record AuthOptions(string Issuer, string Audience, string SecretKey, TimeSpan ExpireTime);
}
s