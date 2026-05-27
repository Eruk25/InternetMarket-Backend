using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Application.Shipments.Get.GetDeliveryPoints
{
    public class DeliveryPointResponse
    {
        public string DeliveryCode { get; set; } = default!;
        public DeliveryLocation Location { get; set; } = default!;
    }
}