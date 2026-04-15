using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Application.Abstractions.UnitOfWork;
using MediatR;

namespace InternetMarket.UserService.Application.EmailVerificationToken.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, bool>
    {
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        public VerifyEmailCommandHandler(IEmailVerificationTokenRepository emailVerificationTokenRepository, IUnitOfWork unitOfWork)
        {
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var token = await _emailVerificationTokenRepository.GetByIdAsync(request.TokenId);

            if (token is null || token.ExpiresAt < DateTime.UtcNow || token.User.IsConfirmed)
                return false;

            token.User.UpdateConfirm(true);

            await _emailVerificationTokenRepository.DeleteAsync(token);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}