using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Update.UpdateUserProfile
{
    public record UpdateUserProfileCommand(Guid Id, string? Name, string? Email) : IRequest;
}