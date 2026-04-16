using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.UserService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.UserService.Application.Users.GetMe
{
    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, GetMyProfileResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetMyProfileQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetMyProfileResponse> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
                throw new InvalidOperationException($"User with id {request.UserId} not found");

            return new GetMyProfileResponse(user.Id, user.Name, user.Email.Value);
        }
    }
}