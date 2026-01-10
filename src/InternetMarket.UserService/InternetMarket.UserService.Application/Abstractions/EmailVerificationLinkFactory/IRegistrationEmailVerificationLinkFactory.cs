namespace InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory
{
    public interface IRegistrationEmailVerificationLinkFactory
    {
        public string GenerateLink(Domain.Entities.EmailVerificationToken token);
    }
}