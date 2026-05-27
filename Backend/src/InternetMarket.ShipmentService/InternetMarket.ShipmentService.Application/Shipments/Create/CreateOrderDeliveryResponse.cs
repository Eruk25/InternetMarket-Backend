using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Application.Shipments.Create
{
    public class CreateOrderDeliveryResponse
    {
        public Guid ShipmentOrderId { get; set; }
        public string State { get; set; } = default!;
    }
}