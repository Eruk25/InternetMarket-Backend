using InternetMarket.Contracts.Events;
using InternetMarket.UserService.Application.Abstractions.EmailVerificationLinkFactory;
using InternetMarket.UserService.Application.Abstractions.PasswordHasher;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Application.Abstractions.UnitOfWork;
using InternetMarket.UserService.Domain.Entities;
using InternetMarket.UserService.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IRegistrationEmailVerificationLinkFactory _emailVerificationLinkFactory;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher,
            IEmailVerificationTokenRepository emailVerificationTokenRepository, IRegistrationEmailVerificationLinkFactory emailVerificationLinkFactory,
            IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _emailVerificationLinkFactory = emailVerificationLinkFactory;
            _passwordHasher = passwordHasher;
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var user = new User(
                request.Name,
                Email.Create(request.Email),
                Password.Create(_passwordHasher.HashPassword(request.Password)));

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

            await _publishEndpoint.Publish(new UserRegistered(
                user.Id,
                user.Email.Value,
                user.Name,
                verificationLink
            ));

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}