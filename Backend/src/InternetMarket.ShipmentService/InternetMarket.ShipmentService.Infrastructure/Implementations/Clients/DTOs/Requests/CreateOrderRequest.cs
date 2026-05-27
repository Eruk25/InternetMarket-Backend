using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Requests
{
    public class CreateOrderRequest
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }
        [JsonPropertyName("tariff_code")]
        public int TariffCode { get; set; }
        [JsonPropertyName("delivery_point")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DeliveryPoint { get; set; }
        [JsonPropertyName("recipient")]
        public Recipient Recipient { get; set; } = default!;
        [JsonPropertyName("from_location")]
        public ShipmentLocation FromLocation { get; set; } = default!;
        [JsonPropertyName("to_location")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ShipmentLocation? ToLocation { get; set; }
        [JsonPropertyName("packages")]
        public IEnumerable<CdekPackage> Packages { get; set; } = default!;
    }
}