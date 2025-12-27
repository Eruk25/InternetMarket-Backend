using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Update
{
    public record UpdateUserCommand(Guid Id, string? Name, string? Password) : IRequest;
}