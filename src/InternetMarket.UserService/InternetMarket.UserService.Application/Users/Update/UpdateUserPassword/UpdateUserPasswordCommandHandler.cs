using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.PasswordHasher;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Domain.Entities;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Update.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IResetPasswordTokenRepository _resetPasswordTokenRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserPasswordCommandHandler(IUserRepository userRepository, IResetPasswordTokenRepository resetPasswordTokenRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _resetPasswordTokenRepository = resetPasswordTokenRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var token = await _resetPasswordTokenRepository.GetByIdAsync(Guid.Parse(request.ResetCode));

            if (token is null)
                throw new Exception("Token invalid or expired");

            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null)
                throw new KeyNotFoundException("User was not found.");

            var hashedPassword = _passwordHasher.HashPassword(request.NewPassword);
            user.UpdatePassword(hashedPassword);

            await _userRepository.UpdateAsync(user);
            await _resetPasswordTokenRepository.DeleteAsync(token);
        }
    }
}