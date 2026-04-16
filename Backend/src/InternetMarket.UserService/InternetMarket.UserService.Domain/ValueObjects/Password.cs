using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Domain.ValueObjects
{
    public class Password
    {
        public string Value { get; }

        private Password(string value) => Value = value;

        public static bool IsValid(string password)
        {
            if (password is null || password.Length < 8)
                return false;
            return true;
        }
        public static Password Create(string password)
        {
            if (!IsValid(password))
                throw new Exception("Password cannot be empty and must be more then 8 symbols");
            return new Password(password);
        }
    }
}