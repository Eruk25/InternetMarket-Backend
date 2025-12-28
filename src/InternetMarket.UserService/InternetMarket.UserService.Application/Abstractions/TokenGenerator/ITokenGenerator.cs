using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Domain;

namespace InternetMarket.UserService.Application.Abstractions.TokenGenerator
{
    public interface ITokenGenerator
    {
        public string GenerateToken(User user);
    }
}