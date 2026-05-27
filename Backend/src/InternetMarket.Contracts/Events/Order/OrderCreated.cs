using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.Contracts.Events.Order.DTOs;

namespace InternetMarket.Contracts.Events.Order
{
    public record OrderCreated(int DeliveryType, int ToCityCode, string? DeliveryPointId, string City, string Address,
     string FullName, string NumberPhone, Guid OrderId, string Email, IEnumerable<OrderItem> Items, decimal TotalPrice);
}