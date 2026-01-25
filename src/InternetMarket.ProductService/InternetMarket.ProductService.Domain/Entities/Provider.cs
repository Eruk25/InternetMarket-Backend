using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.Entities
{
    public class Provider
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string NumberPhone { get; private set; }

        public Provider(string name, string address, string email, string numberPhone)
        {
            Name = name;
            Address = address;
            Email = email;
            NumberPhone = numberPhone;
        }
    }
}