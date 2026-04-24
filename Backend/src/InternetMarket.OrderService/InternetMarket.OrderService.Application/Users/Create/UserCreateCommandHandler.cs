using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using InternetMarket.OrderService.Application.Abstractions.UnitOfWork;
using InternetMarket.OrderService.Domain.Entities;
using InternetMarket.OrderService.Domain.ValueObjects;
using InternetMarket.UserService.Domain.ValueObjects;
using MediatR;

namespace InternetMarket.OrderService.Application.Users.Create
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserCreateCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = new User(
                request.UserId,
                new FullName(request.FirstName, request.LastName),
                Email.Create(request.Email));

            await _userRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}