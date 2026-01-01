using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Domain.Entities;

namespace InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory
{
    public interface IEmailVerificationLinkFactory
    {
        public string GenerateLink(EmailVerificationToken token);
    }
}