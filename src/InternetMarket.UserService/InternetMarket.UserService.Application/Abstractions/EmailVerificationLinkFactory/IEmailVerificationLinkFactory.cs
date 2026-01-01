namespace InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory
{
    public interface IEmailVerificationLinkFactory
    {
        public string GenerateLink(Domain.Entities.EmailVerificationToken token);
    }
}