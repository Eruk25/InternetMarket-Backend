using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects.Category
{
    public class CategoryName
    {
        public string Value { get; private set; }

        private CategoryName(string value) => Value = value;

        public static CategoryName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CategoryName cannot be empty");
            return new CategoryName(value);
        }
    }
}