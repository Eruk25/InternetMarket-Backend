using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Get.GetById
{
    public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto>;
}