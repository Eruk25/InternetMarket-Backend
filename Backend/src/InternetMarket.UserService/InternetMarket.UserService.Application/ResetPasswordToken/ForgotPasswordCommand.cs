using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.ResetPasswordToken
{
    public record ForgotPasswordCommand(string Email) : IRequest;
}