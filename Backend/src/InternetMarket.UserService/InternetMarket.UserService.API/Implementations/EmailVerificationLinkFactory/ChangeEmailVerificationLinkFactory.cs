using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory;

namespace InternetMarket.UserService.API.Implementations.EmailVerificationLinkFactory
{
    public class ChangeEmailVerificationLinkFactory : IChangeEmailVerificationLinkFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public ChangeEmailVerificationLinkFactory(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

        public string GenerateLink(Domain.Entities.EmailVerificationToken emailVerificationToken)
        {
            string? verificationLink = _linkGenerator.GetUriByAction(
                action: "UpdateEmail",
                controller: "Users",
                values: new { id = emailVerificationToken.UserId, token = emailVerificationToken.Id },
                scheme: _httpContextAccessor.HttpContext!.Request.Scheme,
                host: _httpContextAccessor.HttpContext.Request.Host
            );

            return verificationLink ?? throw new Exception("Could not generate link");
        }
    }
}