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
    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint;

        public PayOrderCommandHandler(IOrderRepository orderRepository, IUserRepository userRepository, IUnitOfWork unitOfWork,
         IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
                throw new ArgumentNullException("Заказ не найден.");

            order.Paid();
            var user = await _userRepository.GetByIdAsync(order.UserId);

            if (user is null)
                throw new ArgumentNullException("Пользователь не найден.");

            await _publishEndpoint.Publish(new OrderPaid(order.Id, user.Email.Value));
            await _publishEndpoint.Publish(new OrderCreated(
                order.PaymentMethod.Name,
                order.DeliveryInfo.DeliveryType,
                order.DeliveryInfo.ToCityCode,
                order.DeliveryInfo.DeliveryPointId,
                order.DeliveryInfo.City,
                order.DeliveryInfo.Address,
                order.CustomerName.ToString(),
                order.CustomerPhone.Value,
                order.Id,
                user.Email.Value,
                order.OrderItems.Select(oi => new Contracts.Events.Order.DTOs.OrderItem(
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
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}