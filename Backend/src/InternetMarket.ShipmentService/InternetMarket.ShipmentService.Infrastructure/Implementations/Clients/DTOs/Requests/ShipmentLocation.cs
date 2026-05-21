using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Requests
{
    public class ShipmentLocation
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}