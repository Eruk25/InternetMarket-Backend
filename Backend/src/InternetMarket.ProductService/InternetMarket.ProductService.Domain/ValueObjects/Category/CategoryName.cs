using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects.Category
{
    public record CategoryName
    {
        public string Value { get; private set; }

        private CategoryName(string value) => Value = value;

        public static CategoryName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Название категории не может быть пустым");
            return new CategoryName(value);
        }
    }
}