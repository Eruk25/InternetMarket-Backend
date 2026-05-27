using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Application.Shipments.Get.GetDeliveryPoints
{
    public class DeliveryLocation
    {
        public int CityCode { get; set; }
        public string City { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}