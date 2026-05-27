using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.API.DTOs.Order;

namespace InternetMarket.ShipmentService.API.DTOs
{
    public record CalculateDeliveryRequest
    {
        public int DeliveryType { get; set; }
        public int ToCityCode { get; set; }
        public string? DeliveryPointId { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; } = default!;
    };
}