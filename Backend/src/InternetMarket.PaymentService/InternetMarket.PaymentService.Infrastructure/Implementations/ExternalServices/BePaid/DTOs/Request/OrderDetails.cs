using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs.Request;

namespace InternetMarket.PaymentService.Infrastructure.Implementations.ExternalServices.BePaid.DTOs
{
    public class OrderDetails
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = default!;
        [JsonPropertyName("amount")]
        public long Amount { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; } = default!;
        [JsonPropertyName("tracking_id")]
        public string TrackingId { get; set; } = default!;
        [JsonPropertyName("additional_data")]
        public AdditionalData AdditionalData { get; set; } = default!;
    }
}