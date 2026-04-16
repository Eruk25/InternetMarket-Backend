using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Application.Abstractions.ResetPasswordLinkFactory
{
    public interface IResetPasswordLinkFactory
    {
        public string GenerateLink(Domain.Entities.ResetPasswordToken resetPasswordToken);
    }
}