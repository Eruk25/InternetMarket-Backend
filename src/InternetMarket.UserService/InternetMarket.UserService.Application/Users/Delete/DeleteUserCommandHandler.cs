using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException();

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
                throw new KeyNotFoundException($"User with id {request.Id} was not found");

            await _userRepository.DeleteAsync(user);
        }
    }
}