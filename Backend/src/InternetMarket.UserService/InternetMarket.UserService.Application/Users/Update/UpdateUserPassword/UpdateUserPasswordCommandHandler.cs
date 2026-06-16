using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.PasswordHasher;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Application.Abstractions.UnitOfWork;
using InternetMarket.UserService.Domain.Entities;
using InternetMarket.UserService.Domain.ValueObjects;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Update.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IResetPasswordTokenRepository _resetPasswordTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserPasswordCommandHandler(IUserRepository userRepository, IResetPasswordTokenRepository resetPasswordTokenRepository,
            IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _resetPasswordTokenRepository = resetPasswordTokenRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var token = await _resetPasswordTokenRepository.GetByIdAsync(Guid.Parse(request.ResetCode));

            if (token is null)
                throw new Exception("Токен недействителен или истёк");

            var user = await _userRepository.GetByEmailAsync(Email.Create(request.Email));

            if (user is null)
                throw new KeyNotFoundException("Пользователь не найден");

            var hashedPassword = _passwordHasher.HashPassword(request.NewPassword);
            user.UpdatePassword(Password.Create(hashedPassword));

            await _userRepository.UpdateAsync(user);
            await _resetPasswordTokenRepository.DeleteAsync(token);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}