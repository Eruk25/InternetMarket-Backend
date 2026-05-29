using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.API.DTOs.Order;

namespace InternetMarket.ShipmentService.API.DTOs
{
    public record CreateOrderDeliveryRequest
    {
        public string PaymentMethod { get; set; } = default!;
        public int DeliveryType { get; set; }
        public int ToCityCode { get; set; }
        public string? DeliveryPointId { get; set; }
        public string City { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string NumberPhone { get; set; } = default!;
        public IEnumerable<OrderItemDto> OrderItems { get; set; } = default!;
        public decimal TotalPrice { get; set; }
    }
}