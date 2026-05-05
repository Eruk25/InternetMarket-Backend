using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.OrderService.Domain.Entities;

namespace InternetMarket.OrderService.Application.Orders
{
    public record OrderDto(Guid OrderId, string FirstName, string LastName, string NumberPhone,
     string Address, string City, IEnumerable<OrderItemDto> OrderItems, DateTime CreatedAt, decimal TotalPrice, string Status);
}