using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Order;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using MassTransit;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Delete
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
                throw new Exception("Order was not found");

            order.Cancel();
            await _orderRepository.UpdateAsync(order);

            await _publishEndpoint.Publish(new OrderCancelled(
                order.Id,
                request.Email
            ));
        }
    }
}