using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Application.Shipments.Get
{
    public class ShipmentCityResponse
    {
        public int Code { get; set; }
        public string FullName { get; set; } = default!;
    }
}