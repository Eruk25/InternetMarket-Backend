using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.PasswordHasher;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace InternetMarket.UserService.Infrastructure.Implementations.PasswordHasher
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly Identity.PasswordHasher.PasswordHasher _passwordHasher;

        public PasswordHasher(Identity.PasswordHasher.PasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(hashedPassword, password);
        }
    }
}