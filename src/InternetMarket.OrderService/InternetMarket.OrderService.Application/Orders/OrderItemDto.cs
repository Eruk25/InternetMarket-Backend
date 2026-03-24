using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.Application.Orders
{
    public record OrderItemDto(string Title, int Quantity, decimal UnitPrice, decimal TotalPrice);
}