using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.API.DTOs
{
    public class CreateOrderDeliveryRequest
    {
        public int TypeOfDelivery { get; set; }
        public int CityCode { get; set; }
        public string Address { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string NumberPhone { get; set; } = default!;
    }
}