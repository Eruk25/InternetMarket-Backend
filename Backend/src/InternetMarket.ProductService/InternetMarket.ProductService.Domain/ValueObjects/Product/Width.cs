using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects.Product
{
    public record Width
    {
        public int Value { get; }

        public Width(int value) { Value = value; }

        public static Width Create(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Width can not be equal or less then 0");

            return new Width(value);
        }
    }
}