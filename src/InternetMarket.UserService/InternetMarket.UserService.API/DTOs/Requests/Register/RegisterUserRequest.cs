using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.API.DTOs.Requests.Register
{
    public record RegisterUserRequest(string Name, string Email, string Password);
}