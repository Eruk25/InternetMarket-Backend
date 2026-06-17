using InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory;

namespace InternetMarket.UserService.API.Implementations.EmailVerificationLinkFactory
{
    public class ChangeEmailVerificationLinkFactory : IChangeEmailVerificationLinkFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ChangeEmailVerificationLinkFactory(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public string GenerateLink(Domain.Entities.EmailVerificationToken emailVerificationToken)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var frontendUrl = _configuration["Frontend:BaseUrl"]
                ?? request.Headers["X-Frontend-Url"].FirstOrDefault()
                ?? $"{request.Scheme}://{request.Host}".Replace("5287", "3000");

            var verificationLink = $"{frontendUrl}/email-change?token={emailVerificationToken.Id}&userId={emailVerificationToken.UserId}";

            return verificationLink;
        }
    }
}