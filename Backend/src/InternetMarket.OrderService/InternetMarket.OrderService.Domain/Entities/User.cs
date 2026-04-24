using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Domain.ValueObjects;
using InternetMarket.UserService.Domain.ValueObjects;

namespace InternetMarket.OrderService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public FullName FullName { get; set; }
        public Email Email { get; set; }
        private User() { }
        public User(Guid id, FullName fullName, Email email)
        {
            Id = id;
            FullName = fullName;
            Email = email;
        }
    }
}