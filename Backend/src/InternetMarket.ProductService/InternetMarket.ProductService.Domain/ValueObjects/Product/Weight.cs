using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects.Product
{
    public record Weight
    {
        public int Value { get; }

        private Weight(int value) { Value = value; }

        public static Weight Create(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Вес должен быть больше 0");

            return new Weight(value);
        }
    }
}