using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.EmailVerificationToken.VerifyEmail
{
    public record VerifyEmailCommand(Guid Token) : IRequest;
}