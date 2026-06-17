using InternetMarket.UserService.Application.Abstractions.ResetPasswordLinkFactory;

namespace InternetMarket.UserService.API.Implementations.ResetPasswordLinkFactory
{
    public class ResetPasswordLinkFactory : IResetPasswordLinkFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ResetPasswordLinkFactory(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public string GenerateLink(Domain.Entities.ResetPasswordToken resetPasswordToken)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var frontendUrl = _configuration["Frontend:BaseUrl"]
                ?? request.Headers["X-Frontend-Url"].FirstOrDefault()
                ?? $"{request.Scheme}://{request.Host}".Replace("5287", "3000");

            return $"{frontendUrl}/reset-password?token={resetPasswordToken.Id}";
        }
    }
}
