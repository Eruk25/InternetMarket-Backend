using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Application.Events
{
    public record UserRegistered(Guid Id, string Email, string Name);
}