using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using InternetMarket.ShipmentService.Domain.Entities;
using InternetMarket.ShipmentService.Domain.ValueObjects;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Requests
{
    public class CalculateDeliveryPriceRequest
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }
        [JsonPropertyName("currency")]
        public int Currency { get; set; }
        [JsonPropertyName("tariff_code")]
        public int TariffCode { get; set; }
        [JsonPropertyName("from_location")]
        public ShipmentLocation FromLocation { get; set; } = default!;
        [JsonPropertyName("to_location")]
        public ShipmentLocation ToLocation { get; set; } = default!;
        [JsonPropertyName("packages")]
        public List<Package> Packages { get; set; } = default!;
    }
}