using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Domain.Entities;

namespace InternetMarket.OrderService.Application.Orders
{
    public record OrderDto(Guid OrderId, IEnumerable<OrderItemDto> OrderItems, DateTime CreatedAt);
}