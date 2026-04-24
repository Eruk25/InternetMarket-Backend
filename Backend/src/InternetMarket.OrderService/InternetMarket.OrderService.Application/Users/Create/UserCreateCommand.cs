using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.OrderService.Application.Users.Create
{
    public record UserCreateCommand(Guid UserId, string Email, string FirstName, string LastName) : IRequest;
}