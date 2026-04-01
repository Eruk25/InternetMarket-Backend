using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects
{
    public class ProductName
    {
        public string Value { get; }

        private ProductName(string value) => Value = value;

        public static ProductName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Product name cannot be empty");

            return new ProductName(value);
        }
    }
}