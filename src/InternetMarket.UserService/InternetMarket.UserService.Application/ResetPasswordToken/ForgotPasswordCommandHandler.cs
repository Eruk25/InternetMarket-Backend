using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Password;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Application.Abstractions.ResetPasswordLinkFactory;
using MassTransit;
using MediatR;

namespace InternetMarket.UserService.Application.ResetPasswordToken
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IResetPasswordTokenRepository _resetPasswordTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IResetPasswordLinkFactory _resetPasswordLinkFactory;

        public ForgotPasswordCommandHandler(IResetPasswordTokenRepository resetPasswordTokenRepository, IUserRepository userRepository,
            IResetPasswordLinkFactory resetPasswordLinkFactory, IPublishEndpoint publishEndpoint)
        {
            _resetPasswordTokenRepository = resetPasswordTokenRepository;
            _userRepository = userRepository;
            _resetPasswordLinkFactory = resetPasswordLinkFactory;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null)
                throw new KeyNotFoundException($"User was not found.");

            var token = new Domain.Entities.ResetPasswordToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            };

            await _resetPasswordTokenRepository.CreateAsync(token);
            string verificationLink = _resetPasswordLinkFactory.GenerateLink(token);
            await _publishEndpoint.Publish(new PasswordResetRequested
            {
                Email = user.Email,
                ResetLink = verificationLink
            });
        }
    }
}