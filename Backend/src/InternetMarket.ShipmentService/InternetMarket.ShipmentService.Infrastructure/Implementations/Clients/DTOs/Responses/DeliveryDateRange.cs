using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Responses
{
    public class DeliveryDateRange
    {
        [JsonPropertyName("min")]
        public string Min { get; set; } = default!;
        [JsonPropertyName("max")]
        public string Max { get; set; } = default!;
    }
}