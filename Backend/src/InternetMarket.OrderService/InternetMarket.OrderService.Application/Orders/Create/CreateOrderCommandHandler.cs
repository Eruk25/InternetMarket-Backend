using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Order;
using InternetMarket.OrderService.Application.Abstractions.Clients;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using InternetMarket.OrderService.Application.Abstractions.UnitOfWork;
using InternetMarket.OrderService.Domain.Entities;
using InternetMarket.OrderService.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartServiceClient _cartClient;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnitOfWork _unitOfWork;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICartServiceClient cartClient,
        IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _cartClient = cartClient;
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartClient.GetCartByUserIdAsync(request.UserId);
            if (cart is null || !cart.CartItems.Any())
                throw new ArgumentNullException("Cart is empty");

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
                throw new ArgumentException("User not found");

            var orderItems = cart.CartItems
                .Select(ci => new OrderItem(
                    ci.ProductId,
                    ci.Title,
                    ci.Quantity,
                    ci.Price));

            var order = new Order(request.UserId, user.FullName, NumberPhone.Create(request.NumberPhone),
             Address.Create(request.Street, request.City));
            order.AddItems(orderItems);

            await _orderRepository.CreateAsync(order);
            await _cartClient.ClearCartAsync(request.UserId);

            await _publishEndpoint.Publish(new OrderCreated(
                order.Id,
                user.Email.Value,
                orderItems.Select(oi => new Contracts.Events.Order.DTOs.OrderItem(
                    oi.ProductName,
                    oi.Quantity,
                    oi.UnitPrice
                )),
                order.TotalPrice));

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}