using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Domain.ValueObjects
{
    public class Location
    {
        public string City { get; private set; }
        public string Address { get; private set; }

        private Location(string city, string address)
        {
            City = city;
            Address = address;
        }

        public static Location Create(string city, string street)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(street);
            ArgumentException.ThrowIfNullOrWhiteSpace(city);

            return new Location(city, street);
        }
    }
}