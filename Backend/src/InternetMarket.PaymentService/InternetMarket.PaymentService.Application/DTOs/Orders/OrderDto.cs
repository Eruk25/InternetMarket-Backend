using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Application.DTOs.Orders;

namespace InternetMarket.PaymentService.Application.DTOs
{
    public record OrderDto(Guid OrderId, string FirstName, string LastName, string NumberPhone,
     string Address, string City, List<OrderItemDto> OrderItems, DateTime CreatedAt, decimal TotalPrice);
}