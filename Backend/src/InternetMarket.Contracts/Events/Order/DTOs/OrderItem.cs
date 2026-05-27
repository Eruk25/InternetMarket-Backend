using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.Contracts.Events.Order.DTOs
{
    public record OrderItem(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice, int Weight,
     int Length, int Width, int Height, bool IsLargeSizeProduct);
}