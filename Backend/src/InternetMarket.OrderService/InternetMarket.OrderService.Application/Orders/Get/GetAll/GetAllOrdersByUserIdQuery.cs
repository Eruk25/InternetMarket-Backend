using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace InternetMarket.OrderService.Application.Orders.Get.GetAll
{
    public record GetAllOrdersByUserIdQuery(Guid UserId) : IRequest<IEnumerable<OrderDto>>;
}