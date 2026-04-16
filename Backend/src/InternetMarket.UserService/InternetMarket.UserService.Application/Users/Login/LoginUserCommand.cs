using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Login
{
    public record LoginUserCommand(string Email, string Password) : IRequest<string>;
}