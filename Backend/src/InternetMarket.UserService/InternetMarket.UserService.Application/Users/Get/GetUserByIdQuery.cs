using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Domain;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Get
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
}