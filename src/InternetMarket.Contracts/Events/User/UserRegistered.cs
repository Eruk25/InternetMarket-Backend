using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.Contracts.Events
{
    public record UserRegistered(string Email, string Name, string VerificationLink);
}