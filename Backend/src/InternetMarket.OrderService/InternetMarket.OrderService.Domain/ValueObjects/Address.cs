using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }

        private Address(string street, string city)
        {
            Street = street;
            City = city;
        }

        public static Address Create(string street, string city)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(street);
            ArgumentException.ThrowIfNullOrWhiteSpace(city);

            return new Address(street, city);
        }
    }
}