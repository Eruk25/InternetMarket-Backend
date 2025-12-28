using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Application.Abstractions.PasswordHasher
{
    public interface IPasswordHasher
    {
        public string HashPassword(string Password);
        public bool VerifyPassword(string Password, string HashedPassword);
    }
}