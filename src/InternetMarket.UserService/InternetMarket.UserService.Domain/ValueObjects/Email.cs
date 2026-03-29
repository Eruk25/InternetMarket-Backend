using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Domain.ValueObjects
{
    public class Email
    {
        private static readonly string _emailPattern = @"^[a-z0-9](\.?[a-z0-9]){5,}@g(oogle)?mail?.com$";
        public string Value { get; }

        private Email(string value) => Value = value;

        public static bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, _emailPattern))
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