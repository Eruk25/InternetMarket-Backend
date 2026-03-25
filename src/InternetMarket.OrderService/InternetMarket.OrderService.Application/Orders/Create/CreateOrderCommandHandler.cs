using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Order;
using InternetMarket.OrderService.Application.Abstractions.Clients;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using InternetMarket.OrderService.Domain.Entities;
using MassTransit;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartServiceClient _cartClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICartServiceClient cartClient, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _cartClient = cartClient;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartClient.GetCartByUserIdAsync(request.UserId);
            if (cart is null || !cart.CartItems.Any())
                throw new Exception("Cart is empty");

            var orderItems = cart.CartItems
                .Select(ci => new OrderItem(
                    ci.ProductId,
                    ci.Title,
                    ci.Quantity,
                    ci.Price));

            var order = new Order(request.UserId);
            order.AddItems(orderItems);

            await _orderRepository.CreateAsync(order);
            await _cartClient.ClearCartAsync(request.UserId);

            await _publishEndpoint.Publish(new OrderCreated(
                order.Id,
                request.Email,
                orderItems.Select(oi => new Contracts.Events.Order.DTOs.OrderItem(
                    oi.Title,
                    oi.Quantity,
                    oi.UnitPrice
                )),
                order.TotalPrice
            ));
        }
    }
}