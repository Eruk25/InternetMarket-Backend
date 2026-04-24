using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Domain.ValueObjects
{
    public class FullName
    {
        public string FirstName { get; }
        public string LastName { get; }
        public FullName(string? firstName, string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Name required");

            FirstName = firstName;
            LastName = lastName;
        }

        public string GetShortName() => $"{FirstName} {LastName[0]}.";
        public override string ToString() => $"{FirstName} {LastName}";
    }
}