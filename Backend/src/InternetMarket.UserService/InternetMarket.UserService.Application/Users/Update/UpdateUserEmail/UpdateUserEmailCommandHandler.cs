using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Application.Abstractions.TokenGenerator;
using InternetMarket.UserService.Application.Abstractions.UnitOfWork;
using InternetMarket.UserService.Domain.ValueObjects;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Update.UpdateUserEmail
{
    public class UpdateUserEmailCommandHandler : IRequestHandler<UpdateUserEmailCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;

        public UpdateUserEmailCommandHandler(IUserRepository userRepository,
            IEmailVerificationTokenRepository emailVerificationTokenRepository,
            IUnitOfWork unitOfWork,
            ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var emailVerificationToken = await _emailVerificationTokenRepository.GetByIdAsync(request.Token);

            if (emailVerificationToken is null || emailVerificationToken.ExpiresAt < DateTime.UtcNow)
                throw new InvalidOperationException("Токен недействителен или истёк");

            if (emailVerificationToken.UserId != request.UserId)
                throw new InvalidOperationException("Токен не принадлежит этому пользователю");

            var user = await _userRepository.GetByIdAsync(emailVerificationToken.UserId);

            if (user is null)
                throw new InvalidOperationException("Пользователь не найден");

            user.UpdateEmail(Email.Create(emailVerificationToken.NewEmail));
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _tokenGenerator.GenerateToken(user);
        }
    }
}