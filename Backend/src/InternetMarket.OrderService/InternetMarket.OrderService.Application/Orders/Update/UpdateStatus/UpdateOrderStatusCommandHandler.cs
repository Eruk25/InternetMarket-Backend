using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Order;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using InternetMarket.OrderService.Application.Abstractions.UnitOfWork;
using MassTransit;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Update.UpdateStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository, IUserRepository userRepository, IUnitOfWork unitOfWork,
         IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
                throw new ArgumentNullException("Order was not found");

            order.Paid();
            var user = await _userRepository.GetByIdAsync(order.UserId);

            if (user is null)
                throw new ArgumentNullException("User wan not found");

            await _publishEndpoint.Publish(new OrderPaid(order.Id, user.Email.Value));
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}