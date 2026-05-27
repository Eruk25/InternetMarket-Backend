using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Responses
{
    public class CdekCreateOrderDeliveryResponse
    {
        [JsonPropertyName("entity")]
        public Entity Entity { get; set; } = default!;
        [JsonPropertyName("requests")]
        public IEnumerable<CdekRequest> Requests { get; set; } = default!;
    }
}