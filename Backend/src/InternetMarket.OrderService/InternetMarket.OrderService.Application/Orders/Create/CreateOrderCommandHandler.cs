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
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartServiceClient _cartClient;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductServiceClient _productClient;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICartServiceClient cartClient,
        IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork, IUserRepository userRepository, IProductServiceClient productClient)
        {
            _orderRepository = orderRepository;
            _cartClient = cartClient;
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _productClient = productClient;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartClient.GetCartByUserIdAsync(request.UserId);
            if (cart is null || !cart.CartItems.Any())
                throw new ArgumentNullException("Корзина пуста.");

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
                throw new ArgumentException("Пользователь не найден.");

            if (request.PaymentMethod != PaymentMethod.Card.Name && request.PaymentMethod != PaymentMethod.Cash.Name)
                throw new ArgumentException("Неверный способ оплаты.");
            var paymentMethod = PaymentMethod.FromName(request.PaymentMethod);
            var orderItems = cart.CartItems
                .Select(ci => new OrderItem(
                    ci.ProductId,
                    ci.Title,
                    ci.Quantity,
                    ci.Price,
                    ci.Weight,
                    ci.Length,
                    ci.Width,
                    ci.Height,
                    ci.IsLargeSizeProduct));
            var order = new Order(
                request.UserId,
                user.FullName,
                NumberPhone.Create(request.NumberPhone),
                paymentMethod,
                new DeliveryInfo(request.DeliveryType, request.ToCityCode, request.DeliveryPointId, request.City, request.Address),
                request.DeliveryCost);
            order.AddItems(orderItems);

            await _orderRepository.CreateAsync(order);
            if (paymentMethod == PaymentMethod.Cash)
            {
                await _publishEndpoint.Publish(new OrderCreated(
                request.PaymentMethod,
                request.DeliveryType,
                request.ToCityCode,
                request.DeliveryPointId,
                request.City,
                request.Address,
                request.FullName,
                request.NumberPhone,
                order.Id,
                user.Email.Value,
                orderItems.Select(oi => new Contracts.Events.Order.DTOs.OrderItem(
                    oi.ProductId,
                    oi.ProductName,
                    oi.Quantity,
                    oi.UnitPrice,
                    oi.Weight,
                    oi.Length,
                    oi.Width,
                    oi.Height,
                    oi.IsLargeSizeProduct
                )),
                order.TotalPrice));
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            Dictionary<Guid, int> itemsToReserve = new Dictionary<Guid, int>();
            foreach (var orderItem in order.OrderItems)
            {
                itemsToReserve.Add(orderItem.ProductId, orderItem.Quantity);
            }
            await _productClient.ReserveAsync(itemsToReserve);
            await _cartClient.ClearCartAsync(request.UserId);

            return order.Id;
        }
    }
}