using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects.Product
{
    public record Height
    {
        public int Value { get; }

        private Height(int value) { Value = value; }

        public static Height Create(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Height can not be equal or less then 0");

            return new Height(value);
        }
    }
}