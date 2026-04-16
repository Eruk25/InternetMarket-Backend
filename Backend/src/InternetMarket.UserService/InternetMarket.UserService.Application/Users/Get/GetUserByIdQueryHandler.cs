using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using MediatR;
using InternetMarket.UserService.Domain;

namespace InternetMarket.UserService.Application.Users.Get
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException();

            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
                throw new KeyNotFoundException($"User with id {request.Id} was not found");

            return new UserDto(user.Id, user.Name, user.Email.Value, user.Role.Name);
        }
    }
}