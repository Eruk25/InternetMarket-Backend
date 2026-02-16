using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Email;
using InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using MassTransit;
using MediatR;

namespace InternetMarket.UserService.Application.EmailVerificationToken.EmailChange
{
    public class RequestEmailChangeCommandHandler : IRequestHandler<EmailChangeCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IChangeEmailVerificationLinkFactory _emailVerificationLinkFactory;
        private readonly IUserRepository _userRepository;

        public RequestEmailChangeCommandHandler(IEmailVerificationTokenRepository emailVerificationTokenRepository, IUserRepository userRepository,
            IChangeEmailVerificationLinkFactory emailVerificationLinkFactory, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _emailVerificationLinkFactory = emailVerificationLinkFactory;
            _userRepository = userRepository;
        }

        public async Task Handle(EmailChangeCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null)
                throw new InvalidOperationException("User was not found.");

            var token = new Domain.Entities.EmailVerificationToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            };
            await _emailVerificationTokenRepository.CreateAsync(token);
            var verificationLink = _emailVerificationLinkFactory.GenerateLink(token);
            await _publishEndpoint.Publish(new EmailChangeRequested(
                user.Email,
                verificationLink));
        }
    }
}