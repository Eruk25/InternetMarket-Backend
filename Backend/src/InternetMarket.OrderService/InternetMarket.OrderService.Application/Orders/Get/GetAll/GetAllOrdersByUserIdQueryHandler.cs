using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Application.Abstractions.Repositories;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Get.GetAll
{
    public class GetAllOrdersByUserIdQueryHandler : IRequestHandler<GetAllOrdersByUserIdQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersByUserIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllByUserIdAsync(request.UserId);

            return orders.Select(o => new OrderDto(
                o.Id,
                o.CustomerName.FirstName,
                o.CustomerName.LastName,
                o.CustomerPhone.Value,
                o.CustomerAddress.Street,
                o.CustomerAddress.City,
                o.OrderItems.Select(oi => new OrderItemDto(
                    oi.ProductName,
                    oi.Quantity,
                    oi.UnitPrice,
                    oi.TotalPrice
                )),
                o.CreatedAt,
                o.TotalPrice
            ));
        }
    }
}