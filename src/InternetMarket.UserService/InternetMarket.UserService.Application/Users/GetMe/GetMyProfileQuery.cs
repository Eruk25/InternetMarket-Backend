using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.Users.GetMe
{
    public record GetMyProfileQuery(Guid UserId) : IRequest<GetMyProfileResponse>;
}