using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects
{
    public class Description
    {
        public string Value { get; }

        private Description(string value) => Value = value;

        public static Description Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Description cannot be empty");
            return new Description(value);
        }
    }
}