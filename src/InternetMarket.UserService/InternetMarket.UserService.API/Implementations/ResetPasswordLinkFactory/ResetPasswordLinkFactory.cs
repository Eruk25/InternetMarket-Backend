using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.ResetPasswordLinkFactory;

namespace InternetMarket.UserService.API.Implementations.ResetPasswordLinkFactory
{
    public class ResetPasswordLinkFactory : IResetPasswordLinkFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public ResetPasswordLinkFactory(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

        public string GenerateLink(Domain.Entities.ResetPasswordToken resetPasswordToken)
        {
            string? verificationLink = _linkGenerator.GetUriByAction(
                action: "ResetPassword",
                controller: "AuthController",
                values: new { token = resetPasswordToken.Id },
                scheme: _httpContextAccessor.HttpContext!.Request.Scheme,
                host: _httpContextAccessor.HttpContext.Request.Host);
            return verificationLink ?? throw new Exception("Could not generate link");
        }
    }
}