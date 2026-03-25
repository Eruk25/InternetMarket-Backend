using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.Contracts.Events.Order.DTOs
{
    public record OrderItem(string Title, int Quantity, decimal UnitPrice);
}