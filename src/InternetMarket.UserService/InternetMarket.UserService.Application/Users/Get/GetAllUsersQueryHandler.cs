using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.UserService.Application.Users.Get
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException();

            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserDto(u.Id, u.Name, u.Email, u.Role));
        }
    }
}