using InternetMarket.UserService.Application.Abstractions.ResetPasswordLinkFactory;

namespace InternetMarket.UserService.API.Implementations.ResetPasswordLinkFactory
{
    public class ResetPasswordLinkFactory : IResetPasswordLinkFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResetPasswordLinkFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateLink(Domain.Entities.ResetPasswordToken resetPasswordToken)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var frontendUrl = request.Headers["X-Frontend-Url"].FirstOrDefault()
                ?? $"{request.Scheme}://{request.Host}".Replace("5287", "3000");

            return $"{frontendUrl}/reset-password?token={resetPasswordToken.Id}";
        }
    }
}
