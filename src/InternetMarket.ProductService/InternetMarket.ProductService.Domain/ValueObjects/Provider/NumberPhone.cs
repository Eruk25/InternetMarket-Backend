using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects
{
    public class NumberPhone
    {
        private static readonly string _numberPattern = @"^\+375(17|25|29|33|44|24)\d{7}$";
        public string Value { get; private set; }

        private NumberPhone(string value) => Value = value;

        public static NumberPhone Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, _numberPattern))
                throw new ArgumentException("Invalid number phone format");
            return new NumberPhone(value);
        }
    }
}