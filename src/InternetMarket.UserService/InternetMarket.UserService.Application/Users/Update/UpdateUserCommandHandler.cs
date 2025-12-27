using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException();

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
                throw new KeyNotFoundException($"User with id {request.Id} was not found.");

            if (!string.IsNullOrWhiteSpace(request.Name))
                user.UpdateName(request.Name);
            if (!string.IsNullOrWhiteSpace(request.Password))
                user.UpdatePassword(request.Password);

            await _userRepository.UpdateAsync(user);
        }
    }
}