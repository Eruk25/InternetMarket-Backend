using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Get.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
                throw new ArgumentNullException("Заказ не найден");

            return new OrderDto(
                order.Id,
                order.CustomerName.FirstName,
                order.CustomerName.LastName,
                order.CustomerPhone.Value,
                order.OrderItems.Select(oi => new OrderItemDto(
                    oi.ProductName,
                    oi.Quantity,
                    oi.UnitPrice,
                    oi.TotalPrice
                )),
                order.CreatedAt,
                order.TotalPrice,
                order.Status.Name,
                order.DeliveryInfo.City,
                order.DeliveryInfo.Address);
        }
    }
}