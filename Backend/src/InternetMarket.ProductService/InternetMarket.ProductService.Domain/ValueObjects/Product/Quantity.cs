using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects
{
    public record Quantity
    {
        public int Value { get; }

        private Quantity(int value) => Value = value;

        public static Quantity Create(int value)
        {
            if (value < 0)
                throw new ArgumentException($"Количество должно быть от 0 до {int.MaxValue}");
            return new Quantity(value);
        }

        public Quantity Add(int value) => Create(this.Value + value);
        public Quantity Subtract(int value) => Create(this.Value - value);
    }
}