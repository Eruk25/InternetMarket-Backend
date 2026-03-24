using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.Abstractions.Clients;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using InternetMarket.OrderService.Domain.Entities;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartServiceClient _cartClient;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICartServiceClient cartClient)
        {
            _orderRepository = orderRepository;
            _cartClient = cartClient;
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
        }
    }
}