using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternetMarket.ShipmentService.Infrastructure.Implementations.Clients.DTOs.Responses
{
    public class CdekDeliveryPointResponse
    {
        [JsonPropertyName("code")]
        public string DeliveryCode { get; set; } = default!;
        [JsonPropertyName("location")]
        public CdekDeliveryLocation Location { get; set; } = default!;
    }
}