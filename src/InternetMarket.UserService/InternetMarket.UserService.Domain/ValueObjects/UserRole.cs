using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Ardalis.SmartEnum;
using InternetMarket.UserService.Domain.Entities;

namespace InternetMarket.UserService.Domain.ValueObjects
{
    public class UserRole : SmartEnum<UserRole>
    {
        public static readonly UserRole Client = new UserRole(nameof(Client), 1);
        public static readonly UserRole Buyer = new UserRole(nameof(Buyer), 2);
        public static readonly UserRole Admin = new UserRole(nameof(Admin), 3);

        public UserRole(string name, int value) : base(name, value) { }
    }
}