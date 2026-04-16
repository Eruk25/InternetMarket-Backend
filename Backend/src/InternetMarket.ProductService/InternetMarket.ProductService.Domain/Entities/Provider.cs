using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.ValueObjects;

namespace InternetMarket.ProductService.Domain.Entities
{
    public class Provider
    {
        public Guid Id { get; private set; }
        public ProviderName Name { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }
        public NumberPhone NumberPhone { get; private set; }

        public Provider() { }

        public Provider(ProviderName name, Address address, Email email, NumberPhone numberPhone)
        {
            Name = name;
            Address = address;
            Email = email;
            NumberPhone = numberPhone;
        }
    }
}