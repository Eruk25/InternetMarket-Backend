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
            var request = _httpContextAccessor.HttpContext!.Request;
            var frontendUrl = request.Headers["X-Frontend-Url"].FirstOrDefault()
                ?? $"{request.Scheme}://{request.Host}".Replace("5287", "3000");

            var verificationLink = $"{frontendUrl}/email-change?token={emailVerificationToken.Id}&userId={emailVerificationToken.UserId}";

            return verificationLink;
        }
    }
}