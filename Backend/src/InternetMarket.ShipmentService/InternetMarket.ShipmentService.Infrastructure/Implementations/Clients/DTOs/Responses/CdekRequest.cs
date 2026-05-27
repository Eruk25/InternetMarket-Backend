using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Responses
{
    public class CdekRequest
    {
        [JsonPropertyName("state")]
        public string State { get; set; } = default!;
    }
}