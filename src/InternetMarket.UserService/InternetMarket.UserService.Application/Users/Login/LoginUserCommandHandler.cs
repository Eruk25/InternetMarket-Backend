using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using MediatR;
using System.Runtime.Serialization;
using InternetMarket.UserService.Application.Abstractions.PasswordHasher;
using InternetMarket.UserService.Application.Abstractions.TokenGenerator;
using InternetMarket.UserService.Domain.ValueObjects;

namespace InternetMarket.UserService.Application.Users.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetByEmailAsync(Email.Create(request.Email));

            if (user is null || !_passwordHasher.VerifyPassword(request.Password, user.Password.Value))
                throw new InvalidOperationException("Invalid email or password.");

            return _tokenGenerator.GenerateToken(user);
        }
    }
}