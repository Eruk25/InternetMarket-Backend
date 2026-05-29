using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.OrderService.API.DTOs
{
    public record CreateOrderRequest(string PaymentMethod, int DeliveryType, int ToCityCode, string? DeliveryPointId, string City, string Address,
     string FullName, string NumberPhone);
}