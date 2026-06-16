using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects.Product
{
    public record Length
    {
        public int Value { get; }

        private Length(int value) { Value = value; }

        public static Length Create(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Длина должна быть больше 0");

            return new Length(value);
        }
    }
}