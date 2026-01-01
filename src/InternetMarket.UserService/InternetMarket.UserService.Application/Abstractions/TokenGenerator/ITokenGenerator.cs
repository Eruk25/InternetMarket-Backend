using InternetMarket.UserService.Domain.Entities;

namespace InternetMarket.UserService.Application.Abstractions.TokenGenerator
{
    public interface ITokenGenerator
    {
        public string GenerateToken(User user);
    }
}