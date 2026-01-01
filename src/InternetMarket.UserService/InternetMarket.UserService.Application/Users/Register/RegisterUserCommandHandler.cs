using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.EmailSender;
using InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory;
using InternetMarket.UserService.Application.Abstractions.PasswordHasher;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Application.DTOs.EmailMetadata;
using InternetMarket.UserService.Domain;
using InternetMarket.UserService.Domain.Entities;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IEmailVerificationLinkFactory _emailVerificationLinkFactory;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IEmailService emailService,
            IEmailVerificationTokenRepository emailVerificationTokenRepository, IEmailVerificationLinkFactory emailVerificationLinkFactory)
        {
            _userRepository = userRepository;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _emailVerificationLinkFactory = emailVerificationLinkFactory;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var guid = Guid.NewGuid();
            var user = new User(request.Name, request.Email, _passwordHasher.HashPassword(request.Password), guid);

            await _userRepository.CreateAsync(user);

            var token = new Domain.Entities.EmailVerificationToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            };
            await _emailVerificationTokenRepository.CreateAsync(token);
            string verificationLink = _emailVerificationLinkFactory.GenerateLink(token);
            EmailMetadata emailMetadata = new(user.Email, "From InternetMarket", $"To verify email address <a href={verificationLink}>click here</a>");
            await _emailService.SendAsync(emailMetadata);
        }
    }
}