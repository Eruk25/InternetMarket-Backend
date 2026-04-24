using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Application.Users.GetMe
{
    public record GetMyProfileResponse(Guid Id, string FirstName, string LastName, string Email);
}