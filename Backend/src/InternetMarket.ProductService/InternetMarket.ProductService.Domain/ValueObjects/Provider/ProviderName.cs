using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.ValueObjects
{
    public record ProviderName
    {
        public string Value { get; }

        private ProviderName(string value) => Value = value;

        public static ProviderName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Название поставщика не может быть пустым");
            return new ProviderName(value);
        }
    }
}