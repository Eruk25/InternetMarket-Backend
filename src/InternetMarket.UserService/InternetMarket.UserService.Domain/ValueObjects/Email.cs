using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; }

        private Email(string value) => Value = value;

        public static bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                return false;
            return true;
        }
        public static Email Create(string value)
        {
            if (!IsValid(value))
                throw new Exception("Invalid email format");
            return new Email(value);
        }
    }
}