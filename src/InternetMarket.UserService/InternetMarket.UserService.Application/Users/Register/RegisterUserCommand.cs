using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Register
{
    public record RegisterUserCommand(string Name, string Email, string Password) : IRequest;
}