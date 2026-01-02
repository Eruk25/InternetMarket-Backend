using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Update.UpdateUserPassword
{
    public record UpdateUserPasswordCommand(string Email, string ResetCode, string NewPassword) : IRequest;
}