using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Order;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using InternetMarket.OrderService.Application.Abstractions.UnitOfWork;
using InternetMarket.OrderService.Domain.Entities;
using MassTransit;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Delete
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnitOfWork _unitOfWork;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork,
         IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
                throw new ArgumentNullException("Order was not found");

            var user = await _userRepository.GetByIdAsync(order.UserId);

            if (user is null)
                throw new ArgumentNullException("User was not found");

            order.Cancel();
            await _orderRepository.UpdateAsync(order);

            await _publishEndpoint.Publish(new OrderCancelled(
                order.Id,
                user.Email.Value
            ));

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}