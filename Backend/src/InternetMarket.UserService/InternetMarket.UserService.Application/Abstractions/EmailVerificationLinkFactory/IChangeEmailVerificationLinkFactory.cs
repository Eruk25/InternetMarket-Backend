using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory
{
    public interface IChangeEmailVerificationLinkFactory
    {
        public string GenerateLink(Domain.Entities.EmailVerificationToken token);
    }
}