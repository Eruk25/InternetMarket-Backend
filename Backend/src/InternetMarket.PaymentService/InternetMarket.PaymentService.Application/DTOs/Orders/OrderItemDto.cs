using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.PaymentService.Application.DTOs.Orders
{
    public record OrderItemDto(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice,
     decimal TotalPrice);
}