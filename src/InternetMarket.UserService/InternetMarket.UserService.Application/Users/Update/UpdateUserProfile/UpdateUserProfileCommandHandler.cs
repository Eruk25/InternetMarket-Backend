using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Update.UpdateUserProfile
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserProfileCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
                throw new InvalidOperationException($"User with Id {request.Id} not found.");

            if (!string.IsNullOrWhiteSpace(request.Name))
                user.UpdateName(request.Name);
            if (!string.IsNullOrWhiteSpace(request.Email))
                user.UpdateEmail(request.Email);

            await _userRepository.UpdateAsync(user);
        }
    }
}