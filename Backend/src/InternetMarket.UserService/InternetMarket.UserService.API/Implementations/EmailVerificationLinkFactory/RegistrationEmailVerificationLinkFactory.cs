using InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory;
using InternetMarket.UserService.Domain.Entities;

namespace InternetMarket.UserService.API.Implementations.EmailVerificationLinkFactory
{
    public class RegistrationEmailVerificationLinkFactory : IRegistrationEmailVerificationLinkFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public RegistrationEmailVerificationLinkFactory(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public string GenerateLink(EmailVerificationToken emailVerificationToken)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var frontendUrl = _configuration["Frontend:BaseUrl"]
                ?? request.Headers["X-Frontend-Url"].FirstOrDefault()
                ?? $"{request.Scheme}://{request.Host}".Replace("5287", "3000");

            return $"{frontendUrl}/verify-email?token={emailVerificationToken.Id}";
        }
    }
}
