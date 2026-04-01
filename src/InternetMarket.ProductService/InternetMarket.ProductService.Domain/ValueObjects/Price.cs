using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects
{
    public class Price
    {
        public decimal Value { get; }

        public Price(decimal value) => Value = value;

        public static Price Create(decimal value)
        {
            if (value <= 0 || value <= (decimal)0.9)
                throw new ArgumentException("Price cannot be less then 0 or 0.9 BYN");

            return new Price(value);
        }
    }
}