using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.EmailVerificationToken.EmailChange
{
    public record EmailChangeCommand(Guid UserId, string NewEmail) : IRequest;
}