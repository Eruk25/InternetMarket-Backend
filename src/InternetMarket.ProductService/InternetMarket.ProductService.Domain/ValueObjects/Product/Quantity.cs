using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects
{
    public class Quantity
    {
        public int Value { get; }

        private Quantity(int value) => Value = value;

        public static Quantity Create(int value)
        {
            if (value <= 0 || int.MaxValue < value)
                throw new ArgumentException($"Quantity cannot be equal or less then 0 or more {int.MaxValue}");
            return new Quantity(value);
        }
    }
}