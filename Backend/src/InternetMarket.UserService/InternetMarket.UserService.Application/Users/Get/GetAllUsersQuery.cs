using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Get
{
    public record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;
}