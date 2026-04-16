using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InternetMarket.UserService.Application.Abstractions.TokenGenerator;
using InternetMarket.UserService.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InternetMarket.UserService.Infrastructure.Implementations.JWTGenerator
{
    public class JwtGenerator : ITokenGenerator
    {
        private readonly IOptions<AuthOptions> _options;

        public JwtGenerator(IOptions<AuthOptions> options)
        {
            _options = options;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email.Value),
                new Claim(ClaimTypes.Role, user.Role.Name.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(_options.Value.ExpireTime),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(_options.Value.SecretKey)),
                        SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}