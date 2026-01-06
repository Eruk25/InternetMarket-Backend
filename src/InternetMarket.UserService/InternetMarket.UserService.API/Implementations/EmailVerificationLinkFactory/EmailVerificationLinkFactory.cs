using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory;
using InternetMarket.UserService.Domain.Entities;

namespace InternetMarket.UserService.API.Implementations.EmailVerificationLinkFactory
{
    public class EmailVerificationLinkFactory : IEmailVerificationLinkFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public EmailVerificationLinkFactory(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

        public string GenerateLink(EmailVerificationToken emailVerificationToken)
        {
            string? verificationLink = _linkGenerator.GetUriByAction(
                action: "VerifyEmail",
                controller: "Users",
                values: new { token = emailVerificationToken.Id },
                scheme: _httpContextAccessor.HttpContext!.Request.Scheme,
                host: _httpContextAccessor.HttpContext.Request.Host);
            return verificationLink ?? throw new Exception("Could not generate link");
        }
    }
}