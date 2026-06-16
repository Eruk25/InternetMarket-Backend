using InternetMarket.UserService.Application.Abstractions.PasswordHasher;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using InternetMarket.UserService.Application.Abstractions.UnitOfWork;
using InternetMarket.UserService.Domain.ValueObjects;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Update.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
                throw new KeyNotFoundException($"Пользователь с id {request.UserId} не найден");

            if (!_passwordHasher.VerifyPassword(request.OldPassword, user.Password.Value))
                throw new UnauthorizedAccessException("Текущий пароль неверен");

            var hashedPassword = _passwordHasher.HashPassword(request.NewPassword);
            user.UpdatePassword(Password.Create(hashedPassword));

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
