using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Delete
{
    public record DeleteUserCommand(Guid Id) : IRequest;
}