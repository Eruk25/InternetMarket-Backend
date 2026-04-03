using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects
{
    public class Email
    {
        private static readonly string _emailPattern = @"^[a-z0-9](\.?[a-z0-9]){5,}@g(oogle)?mail?.com$";
        public string Value { get; private set; }

        private Email(string value) => Value = value;

        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, _emailPattern))
                throw new ArgumentException("Invalid Email format");
            return new Email(value);
        }
    }
}