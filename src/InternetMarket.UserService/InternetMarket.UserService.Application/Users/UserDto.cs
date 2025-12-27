using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Application.Users
{
    public record UserDto(Guid Id, string Name, string Email, string Role);
}
