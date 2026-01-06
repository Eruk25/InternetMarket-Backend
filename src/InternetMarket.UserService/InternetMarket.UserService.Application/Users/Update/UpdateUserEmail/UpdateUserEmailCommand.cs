using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Update.UpdateUserEmail
{
    public record UpdateUserEmailCommand(Guid UserId, string Email) : IRequest;
}